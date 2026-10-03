using DeskFlow.Api.DTOs.Categorias;
using DeskFlow.Api.DTOs.Interacoes;
using DeskFlow.Api.Models.Enums;

namespace DeskFlow.Api.DTOs.Chamados
{
    public class ChamadoResponseDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public PrioridadeChamado Prioridade { get; set; }
        public StatusChamado Status { get; set; }
        public string SolicitanteNome { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
        public string? Solucao { get; set; }
        public int CategoriaId { get; set; }
        public CategoriaResponseDTO Categoria { get; set; }
        public IEnumerable<InteracaoResponseDTO> Interacoes { get; set; }
    }
}