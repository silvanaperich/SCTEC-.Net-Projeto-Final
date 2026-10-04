using DeskFlow.Api.DTOs.Categorias;
using DeskFlow.Api.DTOs.Chamados;
using DeskFlow.Api.DTOs.Interacoes;
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

        public async Task AdicionarInteracao(int id, InteracaoCreateDTO interacaoCreateDTO)
        {
            Chamado chamado = await RetornarChamadoPeloId(id);

            if (chamado.Status == StatusChamado.Fechado)
            {
                throw new RegrasException("Chamado está fechado, não é possível adicionar interações.");
            }

            Interacao interacao = new Interacao
            {
                ChamadoId = id,
                DataRegistro = DateTime.Now,
                Autor = interacaoCreateDTO.Autor,
                Mensagem = interacaoCreateDTO.Mensagem
            };
            await _interacoesRepository.Cadastrar(interacao);
        }

        public async Task Atualizar(int id, ChamadoCreateDTO chamadoCreateDTO)
        {
            Chamado chamado = await RetornarChamadoPeloId(id);
            chamado.Atualizar(
                chamadoCreateDTO.Titulo, 
                chamadoCreateDTO.Descricao,
                chamadoCreateDTO.Prioridade,
                chamadoCreateDTO.SolicitanteNome,
                chamadoCreateDTO.CategoriaId);
            await _chamadosRepository.Atualizar(chamado);
        }

        public async Task Cadastrar(ChamadoCreateDTO chamadoCreateDTO)
        {
            Chamado chamado = new Chamado
            {
                Titulo = chamadoCreateDTO.Titulo,
                Descricao = chamadoCreateDTO.Descricao,
                Prioridade = chamadoCreateDTO.Prioridade,
                SolicitanteNome = chamadoCreateDTO.SolicitanteNome,
                CategoriaId = chamadoCreateDTO.CategoriaId,
                DataAbertura = DateTime.Now,
                Status = StatusChamado.Aberto
            };
            await _chamadosRepository.Cadastrar(chamado);
        }

        public async Task EncerrarAtendimento(int id, ChamadoEncerradoUpdateDTO chamadoEncerradoUpdateDTO)
        {
            Chamado chamado = await RetornarChamadoPeloId(id);

            if (chamadoEncerradoUpdateDTO.Solucao.IsNullOrEmpty())
            {
                throw new RegrasException("Necessário informar a solução para o encerramento do chamado.");
            }

            if (chamado.Status == StatusChamado.Fechado)
            {
                throw new RegrasException("Chamado já se encontra no status 'Fechado'.");
            }

            if (chamado.Status == StatusChamado.Aberto)
            {
                throw new RegrasException("Chamado está com status 'Aberto'. Deve ser movimentado para 'EmAndamento' e após para 'Fechado'");
            }

            chamado.Solucao = chamadoEncerradoUpdateDTO.Solucao;
            chamado.Status = StatusChamado.Fechado;
            chamado.DataFechamento = DateTime.Now;
            await _chamadosRepository.Atualizar(chamado);
        }

        public async Task Excluir(int id)
        {
            Chamado chamado = await RetornarChamadoPeloId(id);

            if (chamado != null)
            {
                await _chamadosRepository.Excluir(chamado);
            }
        }

        public async Task IniciarAtendimento(int id)
        {
            Chamado chamado = await RetornarChamadoPeloId(id);

            if (chamado.Status == StatusChamado.Fechado)
            {
                throw new RegrasException("Chamado está fechado, não é possível alterar o status para 'EmAndamento'.");
            }

            chamado.Status = StatusChamado.EmAndamento;
            await _chamadosRepository.Atualizar(chamado);
        }

        public async Task<ChamadoResponseDTO> ObterChamadoPorId(int id)
        {
            Chamado chamado = await RetornarChamadoPeloId(id);

            return new ChamadoResponseDTO
            {
                Id = chamado.Id,
                Titulo = chamado.Titulo,
                Descricao = chamado.Descricao,
                Prioridade = chamado.Prioridade,
                Status = chamado.Status,
                SolicitanteNome = chamado.SolicitanteNome,
                DataAbertura = chamado.DataAbertura,
                DataFechamento = chamado.DataFechamento,
                Solucao = chamado.Solucao,
                CategoriaId = chamado.CategoriaId,
                Categoria = new CategoriaResponseDTO
                {
                    Id = chamado.Categoria.Id,
                    Nome = chamado.Categoria.Nome
                },
                Interacoes = chamado.Interacoes.Select(i => new InteracaoResponseDTO
                {
                    Id = i.Id,
                    Autor = i.Autor,
                    Mensagem = i.Mensagem,
                    DataRegistro = i.DataRegistro
                }).ToList()
            };
        }

        public async Task<List<ChamadoResponseDTO>> ObterChamados(string status, string prioridade, int? categoriaId)
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

            List<Chamado> chamados = await _chamadosRepository.ObterChamados(statusEnum, prioridadeEnum, categoriaId);

            return chamados.Select( ch => new ChamadoResponseDTO
            {
                Id = ch.Id,
                Titulo = ch.Titulo,
                Descricao = ch.Descricao,
                Prioridade = ch.Prioridade,
                Status = ch.Status,
                SolicitanteNome = ch.SolicitanteNome,
                DataAbertura = ch.DataAbertura,
                DataFechamento = ch.DataFechamento,
                Solucao = ch.Solucao,
                CategoriaId = ch.CategoriaId,
                Categoria = new CategoriaResponseDTO
                {
                    Id = ch.Categoria.Id,
                    Nome = ch.Categoria.Nome
                },
                Interacoes = ch.Interacoes.Select(i => new InteracaoResponseDTO
                {
                    Id = i.Id,
                    Autor = i.Autor,
                    Mensagem = i.Mensagem,
                    DataRegistro = i.DataRegistro
                }).ToList()
            }).ToList();
        }
    }
}