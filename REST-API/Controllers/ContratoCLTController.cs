using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restful_API.DTOs;
using Restful_API.Services.Interfaces;

namespace Restful_API.Controllers
{
    [ApiController]
    [Route("api/v1/contratosCLT")]
    public class ContratoCLTController : ControllerBase
    {
        private readonly IContratoCLTService _contratoService;
        private readonly IMapper _mapper;
        public ContratoCLTController(IContratoCLTService contratoService, IMapper mapper)
        {
            _contratoService = contratoService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ContratoCLTDTO>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetContratos()
        {
            var contratosCLT = await _contratoService.GetAllAsync();

            return contratosCLT.Count() == 0 ? NoContent() : Ok(contratosCLT);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContratoCLTDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetContratoById([FromRoute] Guid id)
        {
            var contrato = await _contratoService.GetByIdAsync(id);

            return contrato == null ? NotFound() : Ok(contrato);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ContratoCLTDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateContrato([FromBody] CreateContratoCLTRequest objeto)
        {
            try
            {
                var response = await _contratoService.InsertAsync(objeto);
                if (response == null) return NotFound("Funcionario não encontrado");

                return Created($"api/v1/contratosCLT/{response.Id}", response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContratoCLTDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateContrato([FromRoute] Guid id, [FromBody] UpdateContratoCLTRequest objeto)
        {
            var contrato = await _contratoService.GetByIdAsync(id);

            if (contrato == null) return NotFound();

            if (contrato.Termino != null) return Conflict();

            try
            {
                var response = await _contratoService.UpdateAsync(objeto, id);
                return Ok(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("cancelar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContratoCLTDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CancelContrato([FromRoute] Guid id)
        {
            var contratoToUpdate = await _contratoService.GetByIdAsync(id);

            if (contratoToUpdate == null) return NotFound();

            if (contratoToUpdate.Termino != null) return StatusCode(StatusCodes.Status409Conflict);

            contratoToUpdate.Termino = DateTime.UtcNow;

            var contratoToUpdateRequest = _mapper.Map<UpdateContratoCLTRequest>(contratoToUpdate);

            try
            {
                var response = await _contratoService.UpdateAsync(contratoToUpdateRequest, id);
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
