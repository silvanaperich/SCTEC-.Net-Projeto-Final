using DeskFlow.Api.Models.Entities;
using DeskFlow.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeskFlow.Api.Controllers
{
    [ApiController]
    [Route("interacoes")]
    public class InteracoesController : ControllerBase
    {
        private IInteracoesServices _interacoesServices;

        public InteracoesController(IInteracoesServices interacoesServices)
        {
            _interacoesServices = interacoesServices;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodas()
        {
            List<Interacao> interacoes = await _interacoesServices.ObterTodas();
            return Ok(interacoes);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterInteracaoPorId([FromRoute] int id)
        {
            Interacao interacao = await _interacoesServices.ObterInteracaoPorId(id);
            return Ok(interacao);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] Interacao interacao)
        {
            await _interacoesServices.Cadastrar(interacao);
            return Created("/interacoes", interacao);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Atualizar([FromRoute] int id, [FromBody] Interacao interacaoAtualizada)
        {
            await _interacoesServices.Atualizar(id, interacaoAtualizada);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Excluir([FromRoute] int id)
        {
            await _interacoesServices.Excluir(id);
            return Ok();
        }
    }
}