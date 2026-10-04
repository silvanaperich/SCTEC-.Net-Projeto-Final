using DeskFlow.Api.Data;
using DeskFlow.Api.Models.Entities;
using DeskFlow.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeskFlow.Api.Repositories
{
    public class CategoriasRepository : ICategoriasRepository
    {
        private AppDbContext _context;

        public CategoriasRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Atualizar(Categoria categoria)
        {
             _context.Categorias.Update(categoria);
             await _context.SaveChangesAsync();
        }

        public async Task Cadastrar(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task Excluir(Categoria categoria)
        {
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task<Categoria> ObterCategoriaPorId(int id)
        {
            return await _context.Categorias.FindAsync(id);
        }

        public async Task<List<Categoria>> ObterTodas()
        {
            return await _context.Categorias.ToListAsync();
        }
        
        public async Task<bool> VerificarExisteCategoriaPeloId(int id)
        {
            return await _context.Categorias.AnyAsync(ch => ch.Id == id);
        }
    }
}