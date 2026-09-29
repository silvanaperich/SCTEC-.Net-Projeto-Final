using DeskFlow.Api.Models.Entities;

namespace DeskFlow.Api.Services.Interfaces
{
    public interface IChamadosServices
    {
        Task Cadastrar(Chamado chamado);
        Task Atualizar(int id, Chamado chamadoAtualizado);
        Task Excluir(int id);
        Task<Chamado> ObterChamadoPorId(int id);
        Task<List<Chamado>> ObterChamados(string status, string prioridade, int? categoriaId);
        Task IniciarAtendimento(int id);
        Task EncerrarAtendimento(int id, Chamado chamadoAtualizado);
        Task AdicionarInteracao(int id, Interacao interacao);
    }
}