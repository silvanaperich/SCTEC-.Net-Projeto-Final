using DeskFlow.Api.Exceptions;
using DeskFlow.Api.Models.Entities;
using DeskFlow.Api.Repositories.Interfaces;
using DeskFlow.Api.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace DeskFlow.Api.Services
{
    public class ChamadosServices : IChamadosServices
    {
        private IChamadosRepository _chamadosRepository;
        private IInteracoesRepository _interacoesRepository;

        public ChamadosServices(IChamadosRepository chamadosRepository, IInteracoesRepository interacoesRepository)
        {
            _chamadosRepository = chamadosRepository;
            _interacoesRepository = interacoesRepository;
        }

        public async Task AdicionarInteracao(int id, Interacao interacao)
        {
            Chamado chamado = await _chamadosRepository.ObterChamadoPorId(id);
            
            if (chamado == null)
            {
                throw new KeyNotFoundException("Chamado não encontrado");
            }

            if (chamado.Status == "Fechado")
            {
                throw new RegrasException("Chamado está fechado, não é possível adicionar interações.");
            }

            interacao.ChamadoId = id;
            interacao.DataRegistro = DateTime.Now;;
            await _interacoesRepository.Cadastrar(interacao);
        }

        public async Task Atualizar(int id, Chamado chamadoAtualizado)
        {
            Chamado chamado = await _chamadosRepository.ObterChamadoPorId(id);
            
            if (chamado == null)
            {
                throw new KeyNotFoundException("Chamado não encontrado");
            }
            chamado.Atualizar(chamadoAtualizado);
            await _chamadosRepository.Atualizar(chamado);
        }

        public async Task Cadastrar(Chamado chamado)
        {
            chamado.DataAbertura = DateTime.Now;
            chamado.Status = "Aberto";
            await _chamadosRepository.Cadastrar(chamado);
        }

        public async Task EncerrarAtendimento(int id, Chamado chamadoAtualizado)
        {
            Chamado chamado = await _chamadosRepository.ObterChamadoPorId(id);

            if (chamado == null)
            {
                throw new KeyNotFoundException("Chamado não encontrado");
            }

            if (chamadoAtualizado.Solucao.IsNullOrEmpty())
            {
                throw new RegrasException("Necessário informar a solução para o encerramento do chamado.");
            }

            chamado.Solucao = chamadoAtualizado.Solucao;
            chamado.Status = "Fechado";
            chamado.DataFechamento = DateTime.Now;
            await _chamadosRepository.Atualizar(chamado);
        }

        public async Task Excluir(int id)
        {
            Chamado chamado = await _chamadosRepository.ObterChamadoPorId(id);
            
            if (chamado != null)
            {
                await _chamadosRepository.Excluir(chamado);   
            }
        }

        public async Task IniciarAtendimento(int id)
        {
            Chamado chamado = await _chamadosRepository.ObterChamadoPorId(id);

            if (chamado == null)
            {
                throw new KeyNotFoundException("Chamado não encontrado.");
            }

            chamado.Status = "EmAndamento";
            await _chamadosRepository.Atualizar(chamado);
        }

        public async Task<Chamado> ObterChamadoPorId(int id) => await _chamadosRepository.ObterChamadoPorId(id);

        public async Task<List<Chamado>> ObterChamados(string status, string prioridade, int? categoriaId) => 
            await _chamadosRepository.ObterChamados(status, prioridade, categoriaId);
    }
}