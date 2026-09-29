using DeskFlow.Api.Models.Entities;
using DeskFlow.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeskFlow.Api.Controllers
{
    [ApiController]
    [Route("chamados")]
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
            List<Chamado> chamados = await _chamadosServices.ObterChamados(status, prioridade, categoriaId);
            return Ok(chamados);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterChamadoPorId([FromRoute] int id)
        {
            Chamado chamado = await _chamadosServices.ObterChamadoPorId(id);
            return Ok(chamado);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] Chamado chamado)
        {
            await _chamadosServices.Cadastrar(chamado);
            return Created("/chamados", chamado);
        }

        [HttpPost("{id:int}/iniciar")]
        public async Task<IActionResult> IniciarAtendimento([FromRoute] int id)
        {
            await _chamadosServices.IniciarAtendimento(id);
            return Ok();
        }

        [HttpPost("{id:int}/encerrar")]
        public async Task<IActionResult> EncerrarAtendimento([FromRoute] int id, [FromBody] Chamado chamadoAtualizado)
        {
            await _chamadosServices.EncerrarAtendimento(id, chamadoAtualizado);
            return Ok();
        }

        [HttpPost("{id:int}/interacoes")]
        public async Task<IActionResult> AdicionarInteracao([FromRoute] int id, [FromBody] Interacao interacao)
        {
            await _chamadosServices.AdicionarInteracao(id, interacao);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Atualizar([FromRoute] int id, [FromBody] Chamado chamadoAtualizado)
        {
            await _chamadosServices.Atualizar(id, chamadoAtualizado);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Excluir([FromRoute] int id)
        {
            await _chamadosServices.Excluir(id);
            return Ok();
        }
    }
}