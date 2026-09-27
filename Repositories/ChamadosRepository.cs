using DeskFlow.Api.Data;
using DeskFlow.Api.Models.Entities;
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

        public async Task<Chamado> ObterChamadoPorId(int id) => await _context.Chamados.FindAsync(id);

        public async Task<List<Chamado>> ObterTodos() => await _context.Chamados.ToListAsync();
    }
}