using DeskFlow.Api.DTOs.Categorias;
using DeskFlow.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeskFlow.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/categorias")]
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
            List<CategoriaResponseDTO> categoriaResponseDTOs = await _categoriasServices.ObterTodas();
            return Ok(categoriaResponseDTOs);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterCategoriaPorId([FromRoute] int id)
        {
            CategoriaResponseDTO categoriaResponseDTO = await _categoriasServices.ObterCategoriaPorId(id);
            return Ok(categoriaResponseDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] CategoriaCreateDTO categoriaCreateDTO)
        {
            await _categoriasServices.Cadastrar(categoriaCreateDTO);
            return Created("/categorias", categoriaCreateDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Atualizar([FromRoute] int id, [FromBody] CategoriaCreateDTO categoriaCreateDTO)
        {
            await _categoriasServices.Atualizar(id, categoriaCreateDTO);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Excluir([FromRoute] int id)
        {
            await _categoriasServices.Excluir(id);
            return NoContent();
        }
    }
}