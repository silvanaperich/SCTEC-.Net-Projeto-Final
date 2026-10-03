using DeskFlow.Api.Models.Enums;

namespace DeskFlow.Api.Models.Entities
{
    public class Chamado
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public PrioridadeChamado Prioridade { get; set; }
        public StatusChamado Status { get; set; }
        public string SolicitanteNome { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
        public string? Solucao { get; set; }

        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        public List<Interacao> Interacoes { get; set; } = [];

        public void Atualizar(string titulo, string descricao, PrioridadeChamado prioridade, string solicitanteNome, int categoriaId)
        {
            this.Titulo = titulo;
            this.Descricao = descricao;
            this.Prioridade = prioridade;
            this.SolicitanteNome = solicitanteNome;
            this.CategoriaId = categoriaId;
            this.DataAbertura = DateTime.Now;
            this.Status = StatusChamado.Aberto;
        }
    }
}