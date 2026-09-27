using DeskFlow.Api.Models.Entities;

namespace DeskFlow.Api.Repositories.Interfaces
{
    public interface IInteracoesRepository
    {
        Task Cadastrar(Interacao interacao);
        Task Atualizar(Interacao interacao);
        Task Excluir(Interacao interacao);
        Task<Interacao> ObterInteracaoPorId(int id);
        Task<List<Interacao>> ObterTodas();
    }
}