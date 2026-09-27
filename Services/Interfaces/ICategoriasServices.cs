using DeskFlow.Api.Models.Entities;

namespace DeskFlow.Api.Services.Interfaces
{
    public interface ICategoriasServices
    {
        Task Cadastrar(Categoria categoria);
        Task Atualizar(int id, Categoria categoriaAtualizada);
        Task Excluir(int id);
        Task<Categoria> ObterCategoriaPorId(int id);
        Task<List<Categoria>> ObterTodas();
    }
}