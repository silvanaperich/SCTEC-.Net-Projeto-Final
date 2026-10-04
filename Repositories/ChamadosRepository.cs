using DeskFlow.Api.Data;
using DeskFlow.Api.Models.Entities;
using DeskFlow.Api.Models.Enums;
using DeskFlow.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Chamado>> ObterChamados(StatusChamado? status, PrioridadeChamado? prioridade, int? categoriaId)
        {
            var query = _context.Chamados.AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(c => c.Status == status.Value);
            }

            if (prioridade.HasValue)
            {
                query = query.Where(c => c.Prioridade == prioridade.Value);
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

        public async Task<StatusChamado> RetornarStatusDoChamado(int id)
        {
            return await _context.Chamados
                .Where(ch => ch.Id == id)
                .Select(ch => ch.Status)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> VerificarExisteChamadoPeloId(int id)
        {
            return await _context.Chamados.AnyAsync(ch => ch.Id == id);
        }

        public async Task<bool> VerificarExistemChamadosPorCategoriaId(int categoriaId)
        {
            return await _context.Chamados.AnyAsync(ch => ch.CategoriaId == categoriaId);
        }
    }
}