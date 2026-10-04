using DeskFlow.Api.Data;
using DeskFlow.Api.Models.Entities;
using DeskFlow.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeskFlow.Api.Repositories
{
    public class InteracoesRepository : IInteracoesRepository
    {
        private AppDbContext _context;

        public InteracoesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Atualizar(Interacao interacao)
        {
            _context.Interacoes.Update(interacao);
            await _context.SaveChangesAsync();
        }

        public async Task Cadastrar(Interacao interacao)
        {
            await _context.Interacoes.AddAsync(interacao);
            await _context.SaveChangesAsync();
        }

        public async Task Excluir(Interacao interacao)
        {
            _context.Interacoes.Remove(interacao);
            await _context.SaveChangesAsync();
        }

        public async Task<Interacao> ObterInteracaoPorId(int id) => await _context.Interacoes.FindAsync(id);

        public async Task<List<Interacao>> ObterTodas() => await _context.Interacoes.ToListAsync();
    }
}