using DeskFlow.Api.Models.Entities;
using DeskFlow.Api.Repositories.Interfaces;
using DeskFlow.Api.Services.Interfaces;

namespace DeskFlow.Api.Services
{
    public class CategoriasServices : ICategoriasServices
    {
        private ICategoriasRespository _categoriasRepository;

        public CategoriasServices(ICategoriasRespository categoriasRespository)
        {
            _categoriasRepository = categoriasRespository;
        }
        public async Task Atualizar(int id, Categoria categoriaAtualizada)
        {
            Categoria categoria = await _categoriasRepository.ObterCategoriaPorId(id);

            if (categoria == null)
            {
                //todo: criar execption personalisada
                throw new Exception("Categoria não encontrada");
            }
            categoria.Atualizar(categoriaAtualizada);
            await _categoriasRepository.Atualizar(categoria);
        }

        public async Task Cadastrar(Categoria categoria)
        {
            await _categoriasRepository.Cadastrar(categoria);
        }

        public async Task Excluir(int id)
        {
            Categoria categoria = await _categoriasRepository.ObterCategoriaPorId(id);

            if (categoria != null)
            {
                await _categoriasRepository.Excluir(categoria);
            }
        }

        public async Task<Categoria> ObterCategoriaPorId(int id) => await _categoriasRepository.ObterCategoriaPorId(id);
        public async Task<List<Categoria>> ObterTodas() => await _categoriasRepository.ObterTodas();
    }
}