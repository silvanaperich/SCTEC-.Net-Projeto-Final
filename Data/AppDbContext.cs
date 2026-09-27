using DeskFlow.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeskFlow.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Categoria> Categorias => Set<Categoria>();
        public DbSet<Chamado> Chamados => Set<Chamado>();
        public DbSet<Interacao> Interacoes => Set<Interacao>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(c =>
            {
                c.HasMany(c => c.Chamados)
                 .WithOne(ch => ch.Categoria)
                 .HasForeignKey(ch => ch.CategoriaId);
            });

            modelBuilder.Entity<Chamado>(ch =>
            {
                ch.HasMany(ch => ch.Interacoes)
                  .WithOne(i => i.Chamado)
                  .HasForeignKey(i => i.ChamadoId);    
            });

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Categoria>(categoria =>
            {
                categoria.Property(c => c.Nome)
                         .HasColumnType("varchar(60)")
                         .IsRequired();
            });

            modelBuilder.Entity<Chamado>(chamado =>
            {
                chamado.Property(ch => ch.Titulo)
                       .HasColumnType("varchar(200)")
                       .IsRequired();
                chamado.Property(ch => ch.Descricao)
                       .HasColumnType("varchar(4000)")
                       .IsRequired();
                chamado.Property(ch => ch.Prioridade)
                       .HasColumnType("varchar(25)")
                       .IsRequired();
                chamado.Property(ch => ch.Status)
                       .HasColumnType("varchar(25)")
                       .IsRequired();
                chamado.Property(ch => ch.SolicitanteNome)
                       .HasColumnType("varchar(120)")
                       .IsRequired();
                chamado.Property(ch => ch.Solucao)
                       .HasColumnType("varchar(4000)");
            });

            modelBuilder.Entity<Interacao>(interacao =>
            {
                interacao.Property(i => i.Autor)
                         .HasColumnType("varchar(120)")
                         .IsRequired();
                interacao.Property(i => i.Mensagem)
                         .HasColumnType("varchar(4000)")
                         .IsRequired();
            });
        }
    }
}
