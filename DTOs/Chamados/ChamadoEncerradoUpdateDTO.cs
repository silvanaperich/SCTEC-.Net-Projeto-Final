using System.ComponentModel.DataAnnotations;

namespace DeskFlow.Api.DTOs.Chamados
{
    public class ChamadoEncerradoUpdateDTO
    {
        [Required(ErrorMessage = "A solução é obrigatória.")]
        [StringLength(4000, ErrorMessage = "A solução deve ter até 4000 caracteres.")]
        public string Solucao { get; set; }
    }
}