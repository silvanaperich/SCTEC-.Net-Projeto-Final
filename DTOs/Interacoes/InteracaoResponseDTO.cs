namespace DeskFlow.Api.DTOs.Interacoes
{
    public class InteracaoResponseDTO
    {
        public int Id { get; set; }
        public string Autor { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}