using System.ComponentModel.DataAnnotations;
using DeskFlow.Api.Models.Enums;

namespace DeskFlow.Api.DTOs.Chamados
{
    public class ChamadoCreateDTO
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(200, ErrorMessage = "O título deve ter até 200 caracteres.")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(4000, ErrorMessage = "A descrição deve ter até 4000 caracteres.")]
        public string Descricao { get; set; }
        [EnumDataType(typeof(PrioridadeChamado))]
        public PrioridadeChamado Prioridade { get; set; }
        [Required(ErrorMessage = "O solicitante é obrigatório.")]
        [StringLength(120, ErrorMessage = "O solicitante deve ter até 120 caracteres.")]
        public string SolicitanteNome { get; set; }
        public int CategoriaId { get; set; }
    }
}