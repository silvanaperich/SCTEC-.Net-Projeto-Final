using DeskFlow.Api.Models.Entities;

namespace DeskFlow.Api.Repositories.Interfaces
{
    public interface IChamadosRepository
    {
        Task Cadastrar(Chamado chamado);
        Task Atualizar(Chamado chamado);
        Task Excluir(Chamado chamado);
        Task<Chamado> ObterChamadoPorId(int id);
        Task<List<Chamado>> ObterTodos();
        Task<bool> VerificarExistemChamadosPorCategoriaId(int categoriaId);
    }
}