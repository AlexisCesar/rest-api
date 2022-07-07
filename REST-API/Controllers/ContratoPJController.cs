using Entidades.Models;
using Microsoft.AspNetCore.Mvc;
using Restful_API.Data;
using Restful_API.DTOs;

namespace Restful_API.Controllers
{
    [ApiController]
    [Route("api/v1/contratosPJ")]
    public class ContratoPJController : ControllerBase
    {
        private readonly IContratoPJRepository _contratoRepository;
        private readonly IFuncionarioRepository _funcionarioRepository;

        public ContratoPJController(
            IContratoPJRepository contratoRepository, 
            IFuncionarioRepository funcionarioRepository
        )
        {
            _contratoRepository = contratoRepository;
            _funcionarioRepository = funcionarioRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ContratoPJDTO>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetContratos()
        {
            var contratosPJ = await _contratoRepository.GetContratosAsync();

            return contratosPJ.Count() == 0 ? NoContent() : Ok(contratosPJ);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContratoPJDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetContratoById([FromRoute] Guid id)
        {
            var contrato = await _contratoRepository.GetContratoByIdAsync(id);

            return contrato == null ? NotFound() : Ok(contrato);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ContratoPJDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateContrato([FromBody] CreateContratoPJRequest objeto)
        {
            if(!ModelState.IsValid) return BadRequest();

            var funcionario = await _funcionarioRepository.GetFuncionarioByIdAsync(objeto.FuncionarioId);

            if(funcionario == null) return NotFound();

            var contratoToInsert = new ContratoPJ()
                {
                    Id = Guid.NewGuid(),
                    Inicio = DateTime.UtcNow,
                    Cargo = objeto.Cargo,
                    Salario = objeto.Salario,
                    Funcionario = funcionario
                };
            
            await _contratoRepository.InsertContratoAsync(contratoToInsert);

            try {
                await _contratoRepository.SaveAsync();
            } catch (Exception e) {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Created($"api/v1/contratosPJ/{contratoToInsert.Id}", contratoToInsert);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContratoPJDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateContrato([FromRoute] Guid id, [FromBody] UpdateContratoPJRequest objeto)
        {
            if(!ModelState.IsValid) return BadRequest();

            var contratoToUpdate = await _contratoRepository.GetContratoByIdAsync(id);

            if (contratoToUpdate == null) return NotFound();

            contratoToUpdate.Cargo = objeto.Cargo;
            contratoToUpdate.Salario = objeto.Salario;

            _contratoRepository.UpdateContrato(contratoToUpdate);

            try {
                await _contratoRepository.SaveAsync();
            } catch (Exception e) {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(contratoToUpdate);
        }

        [HttpPut("cancelar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContratoPJDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CancelContrato([FromRoute] Guid id)
        {
            if(!ModelState.IsValid) return BadRequest();

            var contratoToUpdate = await _contratoRepository.GetContratoByIdAsync(id);

            if (contratoToUpdate == null) return NotFound();

            if (contratoToUpdate.Termino != null) return StatusCode(StatusCodes.Status409Conflict);

            contratoToUpdate.Termino = DateTime.UtcNow;
            
            try {
                await _contratoRepository.SaveAsync();
            } catch (Exception e) {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(contratoToUpdate);
        }
    }
}
