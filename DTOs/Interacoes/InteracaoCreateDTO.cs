using System.ComponentModel.DataAnnotations;

namespace DeskFlow.Api.DTOs.Interacoes
{
    public class InteracaoCreateDTO
    {
        [Required(ErrorMessage = "O autor é obrigatório.")]
        [StringLength(120, ErrorMessage = "O autor deve ter até 120 caracteres.")]
        public string Autor { get; set; }
        [Required(ErrorMessage = "A mensagem é obrigatória.")]
        [StringLength(4000, ErrorMessage = "A mensagem deve ter até 4000 caracteres.")]
        public string Mensagem { get; set; }
    }
}