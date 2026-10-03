using System.Text.Json.Serialization;

namespace DeskFlow.Api.Models.Entities
{
    public class Interacao
    {
        public int Id { get; set; }
        public string Autor { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataRegistro { get; set; }

        public int ChamadoId { get; set; }
        [JsonIgnore]
        public Chamado? Chamado { get; set; }

        public void Atualizar(string autor, string mensagem)
        {
            this.Autor = autor;
            this.Mensagem = mensagem;
        }
    }
}