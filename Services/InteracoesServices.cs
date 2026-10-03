using DeskFlow.Api.DTOs.Interacoes;
using DeskFlow.Api.Models.Entities;
using DeskFlow.Api.Repositories.Interfaces;
using DeskFlow.Api.Services.Interfaces;

namespace DeskFlow.Api.Services
{
    public class InteracoesServices : IInteracoesServices
    {
        private IInteracoesRepository _interacoesRepository;

        public InteracoesServices(IInteracoesRepository interacoesRepository)
        {
            _interacoesRepository = interacoesRepository;
        }

        public async Task Atualizar(int id, InteracaoCreateDTO interacaoCreateDTO)
        {
            Interacao interacao = await _interacoesRepository.ObterInteracaoPorId(id);

            if (interacao == null)
            {
                throw new KeyNotFoundException("Interação não encontrada.");
            }

            interacao.Autor = interacaoCreateDTO.Autor;
            interacao.Mensagem = interacaoCreateDTO.Mensagem;
            await _interacoesRepository.Atualizar(interacao);
        }

        public async Task Cadastrar(InteracaoComChamadoIdCreateDTO interacaoComChamadoIdCreateDTO)
        {
            Interacao interacao = new Interacao
            {
                ChamadoId = interacaoComChamadoIdCreateDTO.ChamadoId,
                Autor = interacaoComChamadoIdCreateDTO.Autor,
                Mensagem = interacaoComChamadoIdCreateDTO.Mensagem,
                DataRegistro = DateTime.Now
            };
            await _interacoesRepository.Cadastrar(interacao);
        }

        public async Task Excluir(int id)
        {
            Interacao interacao = await _interacoesRepository.ObterInteracaoPorId(id);

            if (interacao != null)
            {
                await _interacoesRepository.Excluir(interacao);
            }
        }

        public async Task<InteracaoComChamadoIdResponseDTO> ObterInteracaoPorId(int id)
        {
            Interacao interacao = await _interacoesRepository.ObterInteracaoPorId(id);

            if (interacao == null)
            {
                return null;
            }

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