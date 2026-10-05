using System.ComponentModel.DataAnnotations;

namespace DeskFlow.Api.DTOs.Categorias
{
    public class CategoriaCreateDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(60, ErrorMessage = "O nome deve ter até 60 caracteres.")]
        public string Nome { get; set; }
    }
}