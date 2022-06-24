using Entidades.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restful_API.Data;
using Restful_API.ViewModels;
using Restful_API. ViewModels.Common;

namespace Restful_API.Controllers
{
    [ApiController]
    [Route("api/v1/contratos")]
    public class ContratoController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Contrato>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetContratos([FromServices] AppDbContext context)
        {
            var contratosCLT = await context.ContratosCLT.AsNoTracking().ToListAsync();
            var contratosPJ = await context.ContratosPJ.AsNoTracking().ToListAsync();
            var contratos = new List<Contrato>();

            contratosCLT.ForEach(x => contratos.Add(x));
            contratosPJ.ForEach(x => contratos.Add(x));

            return contratos.Count == 0 ? NoContent() : Ok(contratos);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Contrato))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetContratoById([FromServices] AppDbContext context, [FromRoute] Guid id)
        {
            Contrato? contrato = await context.ContratosCLT.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (contrato == null)
            {
                contrato = await context.ContratosCLT.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            }

            return contrato == null ? NotFound() : Ok(contrato);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Contrato))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateContrato([FromServices] AppDbContext context, [FromBody] CreateContratoViewModel objeto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            Funcionario? funcionario = await context.Funcionarios.FirstOrDefaultAsync(x => x.Id == objeto.FuncionarioId);

            if(funcionario == null)
            {
                return NotFound();
            }

            Contrato novoContrato;

            if(objeto.TipoContrato == TipoContratoEnum.CLT)
            {
                novoContrato = new ContratoCLT()
                {
                    Id = Guid.NewGuid(),
                    Inicio = DateTime.UtcNow,
                    Cargo = objeto.Cargo,
                    Salario = objeto.Salario,
                    Funcionario = funcionario
                };
                await context.ContratosCLT.AddAsync((ContratoCLT) novoContrato);
            } else if (objeto.TipoContrato == TipoContratoEnum.PJ)
            {
                novoContrato = new ContratoPJ()
                {
                    Id = Guid.NewGuid(),
                    Inicio = DateTime.UtcNow,
                    Cargo = objeto.Cargo,
                    Salario = objeto.Salario,
                    Funcionario = funcionario
                };
                await context.ContratosPJ.AddAsync((ContratoPJ) novoContrato);
            } else
            {
                return BadRequest();
            }

            try
            {
                await context.SaveChangesAsync();
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Created($"api/v1/contratos/{novoContrato.Id}", novoContrato);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Contrato))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateContrato([FromServices] AppDbContext context, [FromRoute] Guid id, [FromBody] UpdateContratoViewModel objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Contrato? contrato = await context.ContratosCLT.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (contrato == null)
            {
                contrato = await context.ContratosCLT.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            }

            if (contrato == null)
            {
                return NotFound();
            }

            Contrato contratoAtualizado;

            if (objeto.TipoContrato == TipoContratoEnum.CLT)
            {
                contratoAtualizado = new ContratoCLT()
                {
                    Id = contrato.Id,
                    Inicio = contrato.Inicio,
                    Cargo = objeto.Cargo,
                    Salario = objeto.Salario,
                    Funcionario = contrato.Funcionario
                };
                context.ContratosCLT.Update((ContratoCLT)contratoAtualizado);
            }
            else if (objeto.TipoContrato == TipoContratoEnum.PJ)
            {
                contratoAtualizado = new ContratoPJ()
                {
                    Id = contrato.Id,
                    Inicio = contrato.Inicio,
                    Cargo = objeto.Cargo,
                    Salario = objeto.Salario,
                    Funcionario = contrato.Funcionario
                };
                context.ContratosPJ.Update((ContratoPJ)contratoAtualizado);
            }
            else
            {
                return BadRequest();
            }

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(contratoAtualizado);
        }

        [HttpPost]
        [Route("/cancelar/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Contrato))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CancelarContrato([FromServices] AppDbContext context, [FromRoute] Guid id)
        {
            Contrato? contrato = await context.ContratosCLT.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (contrato == null)
            {
                contrato = await context.ContratosCLT.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            }

            if (contrato == null)
            {
                return NotFound();
            }

            if(contrato.Termino != null)
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }

            contrato.Termino = DateTime.UtcNow;

            context.Update(contrato);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(contrato);
        }
    }
}
