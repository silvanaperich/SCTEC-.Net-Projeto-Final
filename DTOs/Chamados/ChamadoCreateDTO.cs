using DeskFlow.Api.Models.Enums;

namespace DeskFlow.Api.DTOs.Chamados
{
    public class ChamadoCreateDTO
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public PrioridadeChamado Prioridade { get; set; }
        public string SolicitanteNome { get; set; }
        public int CategoriaId { get; set; }
    }
}