using DeskFlow.Api.Models.Entities;

namespace DeskFlow.Api.Repositories.Interfaces
{
    public interface ICategoriasRespository
    {
        Task Cadastrar(Categoria categoria);
        Task Atualizar(Categoria categoria);
        Task Excluir(Categoria categoria);
        Task<Categoria> ObterCategoriaPorId(int id);
        Task<List<Categoria>> ObterTodas();
    }
}