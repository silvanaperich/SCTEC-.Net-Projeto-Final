using DeskFlow.Api.Models.Entities;
using DeskFlow.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeskFlow.Api.Controllers
{
    [ApiController]
    [Route("categorias")]
    public class CategoriasController : ControllerBase
    {
        private ICategoriasServices _categoriasServices;

        public CategoriasController(ICategoriasServices categoriasServices)
        {
            _categoriasServices = categoriasServices;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodas()
        {
            List<Categoria> categorias = await _categoriasServices.ObterTodas();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterCategoriaPorId([FromRoute] int id)
        {
            Categoria categoria = await _categoriasServices.ObterCategoriaPorId(id);
            return Ok(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] Categoria categoria)
        {
            await _categoriasServices.Cadastrar(categoria);
            return Created("/categorias", categoria);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar([FromRoute] int id, [FromBody] Categoria categoriaAtualizada)
        {
            await _categoriasServices.Atualizar(id, categoriaAtualizada);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir([FromRoute] int id)
        {
            await _categoriasServices.Excluir(id);
            return Ok();
        }
    }
}