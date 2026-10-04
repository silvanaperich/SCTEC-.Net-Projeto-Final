using DeskFlow.Api.DTOs.Chamados;
using DeskFlow.Api.DTOs.Interacoes;
using DeskFlow.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeskFlow.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/chamados")]
    public class ChamadosController : ControllerBase
    {
        private IChamadosServices _chamadosServices;

        public ChamadosController(IChamadosServices chamadosServices)
        {
            _chamadosServices = chamadosServices;
        }

        [HttpGet]
        public async Task<IActionResult> ObterChamados([FromQuery] string? status, [FromQuery] string? prioridade, [FromQuery] int? categoriaId)
        {
            List<ChamadoResponseDTO> chamadoResponseDTOs = await _chamadosServices.ObterChamados(status, prioridade, categoriaId);
            return Ok(chamadoResponseDTOs);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterChamadoPorId([FromRoute] int id)
        {
            ChamadoResponseDTO chamadoResponseDTO = await _chamadosServices.ObterChamadoPorId(id);
            return Ok(chamadoResponseDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] ChamadoCreateDTO chamadoCreateDTO)
        {
            ChamadoResponseDTO chamadoResponseDTO = await _chamadosServices.Cadastrar(chamadoCreateDTO);
            return CreatedAtAction(nameof(ObterChamadoPorId), new {id = chamadoResponseDTO.Id}, chamadoResponseDTO);
        }

        [HttpPost("{id:int}/iniciar")]
        public async Task<IActionResult> IniciarAtendimento([FromRoute] int id)
        {
            await _chamadosServices.IniciarAtendimento(id);
            return Ok();
        }

        [HttpPost("{id:int}/encerrar")]
        public async Task<IActionResult> EncerrarAtendimento([FromRoute] int id, [FromBody] ChamadoEncerradoUpdateDTO chamadoEncerradoUpdateDTO)
        {
            await _chamadosServices.EncerrarAtendimento(id, chamadoEncerradoUpdateDTO);
            return Ok();
        }

        [HttpPost("{id:int}/interacoes")]
        public async Task<IActionResult> AdicionarInteracao([FromRoute] int id, [FromBody] InteracaoCreateDTO interacaoCreateDTO)
        {
            await _chamadosServices.AdicionarInteracao(id, interacaoCreateDTO);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Atualizar([FromRoute] int id, [FromBody] ChamadoCreateDTO chamadoCreateDTO)
        {
            await _chamadosServices.Atualizar(id, chamadoCreateDTO);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Excluir([FromRoute] int id)
        {
            await _chamadosServices.Excluir(id);
            return NoContent();
        }
    }
}