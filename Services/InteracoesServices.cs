using DeskFlow.Api.DTOs.Interacoes;
using DeskFlow.Api.Exceptions;
using DeskFlow.Api.Models.Entities;
using DeskFlow.Api.Models.Enums;
using DeskFlow.Api.Repositories.Interfaces;
using DeskFlow.Api.Services.Interfaces;

namespace DeskFlow.Api.Services
{
    public class InteracoesServices : IInteracoesServices
    {
        private IInteracoesRepository _interacoesRepository;
        private IChamadosRepository _chamadosRepository;

        public InteracoesServices(IInteracoesRepository interacoesRepository, IChamadosRepository chamadosRepository)
        {
            _interacoesRepository = interacoesRepository;
            _chamadosRepository = chamadosRepository;
        }

        private async Task<Interacao> RetornaInteracaoPeloId(int id)
        {
            Interacao interacao = await _interacoesRepository.ObterInteracaoPorId(id);

            if (interacao == null)
            {
                throw new KeyNotFoundException("Interação não encontrada.");
            }

            return interacao;
        }

        public async Task Atualizar(int id, InteracaoCreateDTO interacaoCreateDTO)
        {
            Interacao interacao = await RetornaInteracaoPeloId(id);
            
            StatusChamado statusChamado = await _chamadosRepository.RetornarStatusDoChamado(interacao.ChamadoId);

            if (statusChamado == StatusChamado.Fechado)
            {
                throw new RegrasException("Chamado está fechado, não é possível alterar a interação.");
            }

            interacao.Atualizar(
                interacaoCreateDTO.Autor,
                interacaoCreateDTO.Mensagem);
            await _interacoesRepository.Atualizar(interacao);
        }

        public async Task<InteracaoComChamadoIdResponseDTO> Cadastrar(InteracaoComChamadoIdCreateDTO interacaoComChamadoIdCreateDTO)
        {
            bool existeChamado = await _chamadosRepository.VerificarExisteChamadoPeloId(interacaoComChamadoIdCreateDTO.ChamadoId);

            if (!existeChamado)
            {
                throw new KeyNotFoundException("Chamado não encontrado para adicionar a interação.");
            }

            StatusChamado statusChamado = await _chamadosRepository.RetornarStatusDoChamado(interacaoComChamadoIdCreateDTO.ChamadoId);

            if (statusChamado == StatusChamado.Fechado)
            {
                throw new RegrasException("Chamado está fechado, não é possível adicionar interações.");
            }

            Interacao interacao = new Interacao
            {
                ChamadoId = interacaoComChamadoIdCreateDTO.ChamadoId,
                Autor = interacaoComChamadoIdCreateDTO.Autor,
                Mensagem = interacaoComChamadoIdCreateDTO.Mensagem,
                DataRegistro = DateTime.UtcNow
            };
            
            await _interacoesRepository.Cadastrar(interacao);

            return new InteracaoComChamadoIdResponseDTO
            {
                Id = interacao.Id,
                ChamadoId = interacao.ChamadoId,
                Autor = interacao.Autor,
                Mensagem = interacao.Mensagem,
                DataRegistro = interacao.DataRegistro
            }; 
        }

        public async Task<InteracaoComChamadoIdResponseDTO> ObterInteracaoPorId(int id)
        {
            Interacao interacao = await RetornaInteracaoPeloId(id);

            return new InteracaoComChamadoIdResponseDTO
            {
                Id = interacao.Id,
                ChamadoId = interacao.ChamadoId,
                Autor = interacao.Autor,
                Mensagem = interacao.Mensagem,
                DataRegistro = interacao.DataRegistro
            };
        } 

        public async Task<List<InteracaoComChamadoIdResponseDTO>> ObterTodas()
        {
            List<Interacao> interacoes = await _interacoesRepository.ObterTodas();

            return interacoes.Select( i => new InteracaoComChamadoIdResponseDTO
            {
                Id = i.Id,
                ChamadoId = i.ChamadoId,
                Autor = i.Autor,
                Mensagem = i.Mensagem,
                DataRegistro = i.DataRegistro
            }).ToList();
        } 
    }
}