using DeskFlow.Api.DTOs.Categorias;
using DeskFlow.Api.Exceptions;
using DeskFlow.Api.Models.Entities;
using DeskFlow.Api.Repositories.Interfaces;
using DeskFlow.Api.Services.Interfaces;

namespace DeskFlow.Api.Services
{
    public class CategoriasServices : ICategoriasServices
    {
        private ICategoriasRepository _categoriasRepository;
        private IChamadosRepository _chamadosRepository;

        public CategoriasServices(ICategoriasRepository categoriasRepository, IChamadosRepository chamadosRepository)
        {
            _categoriasRepository = categoriasRepository;
            _chamadosRepository = chamadosRepository;
        }

        private async Task<Categoria> RetornarCategoriaPorId(int id)
        {
            Categoria categoria = await _categoriasRepository.ObterCategoriaPorId(id);

            if (categoria == null)
            {
                throw new KeyNotFoundException("Categoria não encontrada.");
            }

            return categoria;
        }

        public async Task Atualizar(int id, CategoriaCreateDTO categoriaCreateDTO)
        {
            Categoria categoria = await RetornarCategoriaPorId(id);
            categoria.Atualizar(categoriaCreateDTO.Nome);
            await _categoriasRepository.Atualizar(categoria);
        }

        public async Task<CategoriaResponseDTO> Cadastrar(CategoriaCreateDTO categoriaCreateDTO)
        {
            Categoria categoria = new Categoria {Nome = categoriaCreateDTO.Nome};
            await _categoriasRepository.Cadastrar(categoria);
            return new CategoriaResponseDTO
            {
                Id = categoria.Id, 
                Nome = categoria.Nome
            };
        }

        public async Task Excluir(int id)
        {
            Categoria categoria = await RetornarCategoriaPorId(id);

            if (categoria != null)
            {
                bool possuiChamados = await _chamadosRepository.VerificarExistemChamadosPorCategoriaId(id);

                if (possuiChamados)
                {
                    throw new RegrasException("Categoria possui chamados vinculados, não pode ser excluída.");
                }

                await _categoriasRepository.Excluir(categoria);
            }
        }

        public async Task<CategoriaResponseDTO> ObterCategoriaPorId(int id)
        {
            Categoria categoria = await RetornarCategoriaPorId(id);

            return new CategoriaResponseDTO
            {
                Id = categoria.Id, 
                Nome = categoria.Nome
            };
        }

        public async Task<List<CategoriaResponseDTO>> ObterTodas()
        {
           List<Categoria> categorias = await _categoriasRepository.ObterTodas();

           return categorias.Select(c => new CategoriaResponseDTO
            {
                Id = c.Id, 
                Nome = c.Nome
            }).ToList();
        }
    }
}