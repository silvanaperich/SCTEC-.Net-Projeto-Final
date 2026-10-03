using DeskFlow.Api.DTOs.Interacoes;
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
            List<InteracaoComChamadoIdResponseDTO> interacaoComChamadoIdResponseDTOs = await _interacoesServices.ObterTodas();
            return Ok(interacaoComChamadoIdResponseDTOs);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterInteracaoPorId([FromRoute] int id)
        {
            InteracaoComChamadoIdResponseDTO interacaoComChamadoIdResponseDTO = await _interacoesServices.ObterInteracaoPorId(id);
            return Ok(interacaoComChamadoIdResponseDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] InteracaoComChamadoIdCreateDTO interacaoComChamadoIdCreateDTO)
        {
            await _interacoesServices.Cadastrar(interacaoComChamadoIdCreateDTO);
            return Created("/interacoes", interacaoComChamadoIdCreateDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Atualizar([FromRoute] int id, [FromBody] InteracaoCreateDTO interacaoCreateDTO)
        {
            await _interacoesServices.Atualizar(id, interacaoCreateDTO);
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