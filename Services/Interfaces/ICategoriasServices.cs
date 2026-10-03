using DeskFlow.Api.DTOs.Categorias;

namespace DeskFlow.Api.Services.Interfaces
{
    public interface ICategoriasServices
    {
        Task Cadastrar(CategoriaCreateDTO categoriaCreateDTO);
        Task Atualizar(int id, CategoriaCreateDTO categoriaCreateDTO);
        Task Excluir(int id);
        Task<CategoriaResponseDTO> ObterCategoriaPorId(int id);
        Task<List<CategoriaResponseDTO>> ObterTodas();
    }
}