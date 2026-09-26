namespace DeskFlow.Api.Models.Entities
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        
        public ICollection<Chamado> Chamados {get; set; }
    }
}