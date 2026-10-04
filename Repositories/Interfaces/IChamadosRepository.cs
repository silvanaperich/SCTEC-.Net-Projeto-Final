using DeskFlow.Api.Models.Entities;
using DeskFlow.Api.Models.Enums;

namespace DeskFlow.Api.Repositories.Interfaces
{
    public interface IChamadosRepository
    {
        Task Cadastrar(Chamado chamado);
        Task Atualizar(Chamado chamado);
        Task Excluir(Chamado chamado);
        Task<Chamado> ObterChamadoPorId(int id);
        Task<List<Chamado>> ObterChamados(StatusChamado? status, PrioridadeChamado? prioridade, int? categoriaId);
        Task<bool> VerificarExistemChamadosPorCategoriaId(int categoriaId);
        Task<bool> VerificarExisteChamadoPeloId(int id);
        Task<StatusChamado> RetornarStatusDoChamado(int id);
    }
}