using Microsoft.AspNetCore.Mvc;
using Entidades.Models;
using Restful_API.ViewModels;
using Restful_API.Data;

namespace Restful_API.Controllers
{
    [ApiController]
    [Route("api/v1/colaboradores")]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IFuncionarioContratoRepository _funcionarioContratoRepository;
        public FuncionarioController(
            IFuncionarioRepository funcionarioRepository,
            IFuncionarioContratoRepository funcionarioContratoRepository
            )
        {
            _funcionarioRepository = funcionarioRepository;
            _funcionarioContratoRepository = funcionarioContratoRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FuncionarioViewModel>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetFuncionariosAsync()
        {
            var funcionarios = await _funcionarioRepository.GetFuncionariosAsync();

            return funcionarios.Count() == 0 ? NoContent() : Ok(funcionarios);
        }

        [HttpGet]
        [Route(template:"{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FuncionarioViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFuncionarioByIdAsync([FromRoute] Guid id)
        {
            Funcionario? funcionario = await _funcionarioRepository.GetFuncionarioByIdAsync(id);

            return funcionario == null ? NotFound() : Ok(funcionario);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Funcionario))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateFuncionario([FromBody] CreateFuncionarioViewModel funcionario)
        {
            if(!ModelState.IsValid) return BadRequest();

            Funcionario funcionarioToInsert = new Funcionario()
            {
                Id = Guid.NewGuid(),
                Nome = funcionario.Nome,
                Email = funcionario.Email
            };

            await _funcionarioRepository.InsertFuncionarioAsync(funcionarioToInsert);

            try
            {
                await _funcionarioRepository.SaveAsync();
            } catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Created($"api/v1/colaboradores/{funcionarioToInsert.Id}", funcionarioToInsert);
        }

        [HttpPut]
        [Route(template: "{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FuncionarioViewModel))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateFuncionarioAsync([FromBody] UpdateFuncionarioViewModel objeto, [FromRoute] Guid id)
        {
            if (!ModelState.IsValid) return BadRequest();

            var funcionarioToUpdate = await _funcionarioRepository.GetFuncionarioByIdAsync(id);

            if(funcionarioToUpdate == null) return NotFound();
            
            funcionarioToUpdate.Nome = objeto.Nome;
            funcionarioToUpdate.Email = objeto.Email;

            _funcionarioRepository.UpdateFuncionario(funcionarioToUpdate);

            try
            {
                await _funcionarioRepository.SaveAsync();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(funcionarioToUpdate);
        }

        [HttpDelete]
        [Route(template: "{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FuncionarioViewModel))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteFuncionarioAsync([FromRoute] Guid id)
        {
            var funcionarioToDelete = await _funcionarioRepository.GetFuncionarioByIdAsync(id);

            if (funcionarioToDelete == null) return NotFound();

            await _funcionarioRepository.DeleteFuncionario(id);

            try
            {
                await _funcionarioRepository.SaveAsync();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        [HttpGet]
        [Route(template: "{id}/contratosCLT")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ContratoCLTViewModel>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetContratosCLTFuncionarioByIdAsync([FromRoute] Guid id)
        {
            Funcionario? funcionario = await _funcionarioRepository.GetFuncionarioByIdAsync(id);

            if(funcionario == null) return NotFound();

            var contratosCLT = await _funcionarioContratoRepository.GetContratosCLTAsync(id);

            return contratosCLT.Count() == 0 ? NoContent() : Ok(contratosCLT);
        }

        [HttpGet]
        [Route(template: "{id}/contratosPJ")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ContratoPJViewModel>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetContratosPJFuncionarioByIdAsync([FromRoute] Guid id)
        {
            Funcionario? funcionario = await _funcionarioRepository.GetFuncionarioByIdAsync(id);

            if(funcionario == null) return NotFound();
            
            var contratosPJ = await _funcionarioContratoRepository.GetContratosPJAsync(id);

            return contratosPJ.Count() == 0 ? NoContent() : Ok(contratosPJ);
        }
    }
}
