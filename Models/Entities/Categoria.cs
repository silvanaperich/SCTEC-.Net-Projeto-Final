using System.Text.Json.Serialization;

namespace DeskFlow.Api.Models.Entities
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        [JsonIgnore]
        public ICollection<Chamado> Chamados {get; set; } = [];

        public void Atualizar(Categoria categoria)
        {
            this.Nome = categoria.Nome;
        }
    }
}