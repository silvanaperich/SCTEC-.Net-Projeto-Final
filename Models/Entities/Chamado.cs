namespace DeskFlow.Api.Models.Entities
{
    public class Chamado
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Prioridade { get; set; }
        public string Status { get; set; }
        public string SolicitanteNome { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
        public string Solucao { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        
        public List<Interacao> Interacoes { get; set; } = [];
    }
}