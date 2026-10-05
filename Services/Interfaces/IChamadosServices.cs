using DeskFlow.Api.DTOs.Chamados;
using DeskFlow.Api.DTOs.Interacoes;

namespace DeskFlow.Api.Services.Interfaces
{
    public interface IChamadosServices
    {
        Task<ChamadoResponseDTO> Cadastrar(ChamadoCreateDTO chamadoCreateDTO);
        Task Atualizar(int id, ChamadoCreateDTO chamadoCreateDTO);
        Task<ChamadoResponseDTO> ObterChamadoPorId(int id);
        Task<List<ChamadoResponseDTO>> ObterChamados(string status, string prioridade, int? categoriaId);
        Task IniciarAtendimento(int id);
        Task EncerrarAtendimento(int id, ChamadoEncerradoUpdateDTO chamadoEncerradoUpdateDTO);
        Task<InteracaoResponseDTO> AdicionarInteracao(int id, InteracaoCreateDTO interacaoCreateDTO);
    }
}