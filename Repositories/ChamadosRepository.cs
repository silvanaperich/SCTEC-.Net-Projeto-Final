using DeskFlow.Api.Data;
using DeskFlow.Api.Models.Entities;
using DeskFlow.Api.Models.Enums;
using DeskFlow.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DeskFlow.Api.Repositories
{
    public class ChamadosRepository : IChamadosRepository
    {
        private AppDbContext _context;

        public ChamadosRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Atualizar(Chamado chamado)
        {
            _context.Chamados.Update(chamado);
            await _context.SaveChangesAsync();
        }

        public async Task Cadastrar(Chamado chamado)
        {
            await _context.Chamados.AddAsync(chamado);
            await _context.SaveChangesAsync();
        }

        public async Task Excluir(Chamado chamado)
        {
            _context.Chamados.Remove(chamado);
            await _context.SaveChangesAsync();
        }

        public async Task<Chamado> ObterChamadoPorId(int id)
        {
            return await _context.Chamados
                .Include(c => c.Categoria)
                .Include(c => c.Interacoes)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Chamado>> ObterChamados(string status, string prioridade, int? categoriaId)
        {
            var query = _context.Chamados.AsQueryable();

            if (!status.IsNullOrEmpty())
            {
                if (!Enum.TryParse<StatusChamado>(status, ignoreCase: true, out var statusEnum))
                {
                    throw new ArgumentException($"Status '{status}' inválido.");
                }                
                query = query.Where(c => c.Status == statusEnum);
            }

            if (!prioridade.IsNullOrEmpty())
            {
                if (!Enum.TryParse<PrioridadeChamado>(prioridade, ignoreCase: true, out var prioridadeEnum))
                {
                    throw new ArgumentException($"Prioridade '{prioridade}' inválido.");
                }          
                query = query.Where(c => c.Prioridade == prioridadeEnum);
            }

            if (categoriaId.HasValue)
            {
                query = query.Where(c => c.CategoriaId == categoriaId);
            }

            return await query
                .Include(c => c.Categoria)
                .Include(c => c.Interacoes)
                .ToListAsync();   
        }

        public async Task<bool> VerificarExistemChamadosPorCategoriaId(int categoriaId)
        {
            return await _context.Chamados.AnyAsync(ch => ch.CategoriaId == categoriaId);
        }
    }
}