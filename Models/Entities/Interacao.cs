namespace DeskFlow.Api.Models.Entities
{
    public class Interacao
    {
        public int Id { get; set; }
        public string Autor { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataRegistro { get; set; }

        public int ChamadoId { get; set; }
        public Chamado Chamado { get; set; }
    }
}