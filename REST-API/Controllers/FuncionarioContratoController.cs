using Microsoft.AspNetCore.Mvc;
using Restful_API.DTOs;
using Restful_API.Services.Interfaces;

namespace Restful_API.Controllers
{
    [ApiController]
    [Route("api/v1/colaboradores")]
    public class FuncionarioContratoController : ControllerBase
    {
        private readonly IFuncionarioContratoService _service;

        public FuncionarioContratoController(IFuncionarioContratoService service)
        {
            _service = service;
        }

        [HttpGet("{funcionarioId}/contratosCLT")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContratoCLTDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetContratosCLT([FromRoute] Guid funcionarioId)
        {
            var contratos = await _service.GetContratosCLTAsync(funcionarioId);
            return contratos.Count() == 0 ? NoContent() : Ok(contratos);
        }

        [HttpGet("{funcionarioId}/contratosPJ")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContratoPJDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetContratosPJ([FromRoute] Guid funcionarioId)
        {
            var contratos = await _service.GetContratosPJAsync(funcionarioId);
            return contratos.Count() == 0 ? NoContent() : Ok(contratos);
        }
    }
}