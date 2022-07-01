using Microsoft.AspNetCore.Mvc;
using Entidades.Models;
using Restful_API.ViewModels;
using Restful_API.Data;
using Microsoft.EntityFrameworkCore;

namespace Restful_API.Controllers
{
    [ApiController]
    [Route("api/v1/colaboradores")]
    public class FuncionarioController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Funcionario>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetFuncionariosAsync([FromServices] AppDbContext context)
        {
            var funcionarios = await context.Funcionarios.AsNoTracking().ToListAsync();

            return funcionarios.Count == 0 ? NoContent() : Ok(funcionarios);
        }

        [HttpGet]
        [Route(template:"{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Funcionario))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFuncionarioByIdAsync([FromServices] AppDbContext context, [FromRoute] Guid id)
        {
            Funcionario? funcionario = await context.Funcionarios.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            return funcionario == null ? NotFound() : Ok(funcionario);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Funcionario))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateFuncionario([FromServices] AppDbContext context, [FromBody] CreateFuncionarioViewModel funcionario)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            Funcionario funcionarioCriado = new Funcionario()
            {
                Id = Guid.NewGuid(),
                Nome = funcionario.Nome,
                Email = funcionario.Email
            };

            await context.Funcionarios.AddAsync(funcionarioCriado);

            try
            {
                await context.SaveChangesAsync();
            } catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Created($"api/v1/colaboradores/{funcionarioCriado.Id}", funcionarioCriado);
        }

        [HttpPut]
        [Route(template: "{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Funcionario))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateFuncionarioAsync([FromServices] AppDbContext context, [FromBody] UpdateFuncionarioViewModel objeto, [FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Funcionario? funcionario = await context.Funcionarios.FirstOrDefaultAsync(x => x.Id == id);

            if(funcionario == null)
            {
                return NotFound();
            }

            try
            {
                funcionario.Nome = objeto.Nome;
                funcionario.Email = objeto.Email;

                context.Funcionarios.Update(funcionario);

                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(funcionario);
        }

        [HttpDelete]
        [Route(template: "{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Funcionario))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteFuncionarioAsync([FromServices] AppDbContext context, [FromRoute] Guid id)
        {
            Funcionario? funcionario = await context.Funcionarios.FirstOrDefaultAsync(x => x.Id == id);

            if (funcionario == null)
            {
                return NotFound();
            }

            try
            {
                context.Funcionarios.Remove(funcionario);

                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        [HttpGet]
        [Route(template: "{id}/contratos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Contrato>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetContratosFuncionarioByIdAsync([FromServices] AppDbContext context, [FromRoute] Guid id)
        {
            Funcionario? funcionario = await context.Funcionarios.FirstOrDefaultAsync(x => x.Id == id);

            if (funcionario == null)
            {
                return NotFound();
            }

            var contratosCLT = await context.ContratosCLT.AsNoTracking().Where(x => x.Funcionario.Id == id).ToListAsync();
            var contratosPJ = await context.ContratosPJ.AsNoTracking().Where(x => x.Funcionario.Id == id).ToListAsync();
            var contratos = new List<Contrato>();

            contratosCLT.ForEach(x => contratos.Add(x));
            contratosPJ.ForEach(x => contratos.Add(x));

            return contratos.Count == 0 ? NoContent() : Ok(contratos);
        }
    }
}
