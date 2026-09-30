using DeskFlow.Api.Exceptions;
using DeskFlow.Api.Models.Entities;
using DeskFlow.Api.Models.Enums;
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

        private async Task<Chamado> RetornarChamadoPeloId(int id)
        {
            Chamado chamado = await _chamadosRepository.ObterChamadoPorId(id);

            if (chamado == null)
            {
                throw new KeyNotFoundException("Chamado não encontrado.");
            }

            return chamado;
        }

        public async Task AdicionarInteracao(int id, Interacao interacao)
        {
            Chamado chamado = await RetornarChamadoPeloId(id);

            if (chamado.Status == StatusChamado.Fechado)
            {
                throw new RegrasException("Chamado está fechado, não é possível adicionar interações.");
            }

            interacao.ChamadoId = id;
            interacao.DataRegistro = DateTime.Now;;
            await _interacoesRepository.Cadastrar(interacao);
        }

        public async Task Atualizar(int id, Chamado chamadoAtualizado)
        {
            Chamado chamado = await RetornarChamadoPeloId(id);
            chamado.Atualizar(chamadoAtualizado);
            await _chamadosRepository.Atualizar(chamado);
        }

        public async Task Cadastrar(Chamado chamado)
        {
            chamado.DataAbertura = DateTime.Now;
            chamado.Status = StatusChamado.Aberto;
            await _chamadosRepository.Cadastrar(chamado);
        }

        public async Task EncerrarAtendimento(int id, Chamado chamadoAtualizado)
        {
            Chamado chamado = await RetornarChamadoPeloId(id);

            if (chamadoAtualizado.Solucao.IsNullOrEmpty())
            {
                throw new RegrasException("Necessário informar a solução para o encerramento do chamado.");
            }

            chamado.Solucao = chamadoAtualizado.Solucao;
            chamado.Status = StatusChamado.Fechado;
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
            Chamado chamado = await RetornarChamadoPeloId(id);
            chamado.Status = StatusChamado.EmAndamento;
            await _chamadosRepository.Atualizar(chamado);
        }

        public async Task<Chamado> ObterChamadoPorId(int id) => await _chamadosRepository.ObterChamadoPorId(id);

        public async Task<List<Chamado>> ObterChamados(string status, string prioridade, int? categoriaId)
        {
            StatusChamado? statusEnum = null;
            PrioridadeChamado? prioridadeEnum = null;

            if (!status.IsNullOrEmpty())
            {
                if (!Enum.TryParse<StatusChamado>(status, ignoreCase: true, out var statusEnumChecado))
                {
                    throw new ArgumentException($"Status '{status}' inválido.");
                }
                statusEnum = statusEnumChecado;
            }

            if (!prioridade.IsNullOrEmpty())
            {
                if (!Enum.TryParse<PrioridadeChamado>(prioridade, ignoreCase: true, out var prioridadeEnumChecado))
                {
                    throw new ArgumentException($"Prioridade '{prioridade}' inválido.");
                }
                prioridadeEnum = prioridadeEnumChecado;
            }

            return await _chamadosRepository.ObterChamados(statusEnum, prioridadeEnum, categoriaId);
        }
    }
}