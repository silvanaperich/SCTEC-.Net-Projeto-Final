using DeskFlow.Api.Models.Entities;

namespace DeskFlow.Api.Services.Interfaces
{
    public interface IInteracoesServices
    {
        Task Cadastrar(Interacao interacao);
        Task Atualizar(int id, Interacao interacaoAtualizada);
        Task Excluir(int id);
        Task<Interacao> ObterInteracaoPorId(int id);
        Task<List<Interacao>> ObterTodas();
    }
}