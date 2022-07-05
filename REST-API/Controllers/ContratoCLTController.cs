using Entidades.Models;
using Microsoft.AspNetCore.Mvc;
using Restful_API.Data;
using Restful_API.ViewModels;

namespace Restful_API.Controllers
{
    [ApiController]
    [Route("api/v1/contratosCLT")]
    public class ContratoCLTController : ControllerBase
    {
        private readonly IContratoCLTRepository _contratoRepository;
        private readonly IFuncionarioRepository _funcionarioRepository;

        public ContratoCLTController(
            IContratoCLTRepository contratoRepository, 
            IFuncionarioRepository funcionarioRepository
        )
        {
            _contratoRepository = contratoRepository;
            _funcionarioRepository = funcionarioRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ContratoCLTViewModel>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetContratos()
        {
            var contratosCLT = await _contratoRepository.GetContratosAsync();

            return contratosCLT.Count() == 0 ? NoContent() : Ok(contratosCLT);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContratoCLTViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetContratoById([FromRoute] Guid id)
        {
            var contrato = await _contratoRepository.GetContratoByIdAsync(id);

            return contrato == null ? NotFound() : Ok(contrato);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ContratoCLTViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateContrato([FromBody] CreateContratoCLTViewModel objeto)
        {
            if(!ModelState.IsValid) return BadRequest();

            var funcionario = await _funcionarioRepository.GetFuncionarioByIdAsync(objeto.FuncionarioId);

            if(funcionario == null) return NotFound();

            var contratoToInsert = new ContratoCLT()
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

            return Created($"api/v1/contratosCLT/{contratoToInsert.Id}", contratoToInsert);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContratoCLTViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateContrato([FromRoute] Guid id, [FromBody] UpdateContratoCLTViewModel objeto)
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContratoCLTViewModel))]
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
