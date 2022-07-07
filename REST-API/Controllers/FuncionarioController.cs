using Microsoft.AspNetCore.Mvc;
using Restful_API.DTOs;
using Restful_API.Services.Interfaces;

namespace Restful_API.Controllers
{
    [ApiController]
    [Route("api/v1/colaboradores")]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioService _funcionarioService;
        public FuncionarioController(IFuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FuncionarioDTO>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetFuncionariosAsync()
        {
            var funcionarios = await _funcionarioService.GetAllAsync();

            return funcionarios.Count() == 0 ? NoContent() : Ok(funcionarios);
        }

        [HttpGet]
        [Route(template:"{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FuncionarioDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFuncionarioByIdAsync([FromRoute] Guid id)
        {
            var funcionario = await _funcionarioService.GetByIdAsync(id);

            return funcionario == null ? NotFound() : Ok(funcionario);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FuncionarioDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateFuncionario([FromBody] CreateFuncionarioRequest funcionario)
        {
            try
            {
                var insertedFuncionario = await _funcionarioService.InsertAsync(funcionario);
                return Created($"api/v1/colaboradores/{insertedFuncionario.Id}", insertedFuncionario);
            } catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [Route(template: "{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FuncionarioDTO))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateFuncionarioAsync([FromBody] UpdateFuncionarioRequest objeto, [FromRoute] Guid id)
        {
            if(await _funcionarioService.GetByIdAsync(id) == null) return NotFound();

            try
            {
                var updatedFuncionario = await _funcionarioService.UpdateAsync(objeto, id);
                return Ok(updatedFuncionario);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route(template: "{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteFuncionarioAsync([FromRoute] Guid id)
        {
            if (await _funcionarioService.GetByIdAsync(id) == null) return NotFound();

            try
            {
                await _funcionarioService.DeleteAsync(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

    }
}
