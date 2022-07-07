using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restful_API.DTOs;
using Restful_API.Services.Interfaces;

namespace Restful_API.Controllers
{
    [ApiController]
    [Route("api/v1/contratosPJ")]
    public class ContratoPJController : ControllerBase
    {
        private readonly IContratoPJService _contratoService;
        private readonly IMapper _mapper;
        public ContratoPJController(IContratoPJService contratoService, IMapper mapper)
        {
            _contratoService = contratoService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ContratoPJDTO>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetContratos()
        {
            var contratosPJ = await _contratoService.GetAllAsync();

            return contratosPJ.Count() == 0 ? NoContent() : Ok(contratosPJ);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContratoPJDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetContratoById([FromRoute] Guid id)
        {
            var contrato = await _contratoService.GetByIdAsync(id);

            return contrato == null ? NotFound() : Ok(contrato);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ContratoPJDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateContrato([FromBody] CreateContratoPJRequest objeto)
        {
            try
            {
                var response = await _contratoService.InsertAsync(objeto);
                return Created($"api/v1/contratosPJ/{response.Id}", response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContratoPJDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateContrato([FromRoute] Guid id, [FromBody] UpdateContratoPJRequest objeto)
        {
            if (await _contratoService.GetByIdAsync(id) == null) return NotFound();

            try
            {
                var response = await _contratoService.UpdateAsync(objeto, id);
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("cancelar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContratoPJDTO))]
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

            var contratoToUpdateRequest = _mapper.Map<UpdateContratoPJRequest>(contratoToUpdate);

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
