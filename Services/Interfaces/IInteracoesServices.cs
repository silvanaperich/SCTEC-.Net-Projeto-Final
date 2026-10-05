using DeskFlow.Api.DTOs.Interacoes;

namespace DeskFlow.Api.Services.Interfaces
{
    public interface IInteracoesServices
    {
        Task<InteracaoComChamadoIdResponseDTO> Cadastrar(InteracaoComChamadoIdCreateDTO interacaoComChamadoIdCreateDTO);
        Task Atualizar(int id, InteracaoCreateDTO interacaoCreateDTO);
        Task<InteracaoComChamadoIdResponseDTO> ObterInteracaoPorId(int id);
        Task<List<InteracaoComChamadoIdResponseDTO>> ObterTodas();
    }
}