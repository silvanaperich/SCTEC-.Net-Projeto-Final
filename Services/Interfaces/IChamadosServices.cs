using DeskFlow.Api.Models.Entities;

namespace DeskFlow.Api.Services.Interfaces
{
    public interface IChamadosServices
    {
        Task Cadastrar(Chamado chamado);
        Task Atualizar(int id, Chamado chamadoAtualizado);
        Task Excluir(int id);
        Task<Chamado> ObterChamadoPorId(int id);
        Task<List<Chamado>> ObterTodos();
        Task IniciarAtendimento(int id);
        Task EncerrarAtendimento(int id);
    }
}