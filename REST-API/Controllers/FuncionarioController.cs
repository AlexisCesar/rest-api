﻿using Microsoft.AspNetCore.Mvc;
using Entidades.Models;
using Restful_API.ViewModels;
using Restful_API.ViewModels.Common;
using Restful_API.Data;
using Microsoft.EntityFrameworkCore;

namespace Restful_API.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class FuncionarioController : ControllerBase
    {
        [HttpGet]
        [Route("colaboradores")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Funcionario))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetFuncionariosAsync([FromServices] AppDbContext context)
        {
            var funcionariosCLT = await context.FuncionariosCLT.AsNoTracking().ToListAsync();
            var funcionariosPJ = await context.FuncionariosPJ.AsNoTracking().ToListAsync();

            var funcionarios = new List<Funcionario>();

            funcionariosCLT.ForEach(x => {
                funcionarios.Add(x);
            });

            funcionariosPJ.ForEach(x => {
                funcionarios.Add(x);
            });

            return funcionarios.Count == 0 ? NoContent() : Ok(funcionarios);
        }

        [HttpGet]
        [Route(template:"colaboradores/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Funcionario))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFuncionarioByIdAsync([FromServices] AppDbContext context, [FromRoute] Guid id)
        {
            Funcionario? funcionario = await context.FuncionariosCLT.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if(funcionario == null)
            {
                funcionario = await context.FuncionariosPJ.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            }

            return funcionario == null ? NotFound() : Ok(funcionario);
        }

        [HttpPost]
        [Route("colaboradores")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Funcionario))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateFuncionario([FromServices] AppDbContext context, [FromBody] CreateFuncionarioViewModel funcionario)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            Funcionario funcionarioCriado;

            if(funcionario.TipoContrato == TipoContratoEnum.CLT)
            {
                funcionarioCriado = new FuncionarioCLT()
                {
                    Id = Guid.NewGuid(),
                    Nome = funcionario.Nome,
                    Salario = funcionario.Salario,
                };

                await context.FuncionariosCLT.AddAsync((FuncionarioCLT) funcionarioCriado);

            } else if (funcionario.TipoContrato == TipoContratoEnum.PJ)
            {
                funcionarioCriado = new FuncionarioPJ()
                {
                    Id = Guid.NewGuid(),
                    Nome = funcionario.Nome,
                    Salario = funcionario.Salario,
                };

                await context.FuncionariosPJ.AddAsync((FuncionarioPJ)funcionarioCriado);

            } else
            {
                return BadRequest();
            }

            try
            {
                await context.SaveChangesAsync();
            } catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Created($"api/v1/colaboradores/{funcionarioCriado.Id}", funcionarioCriado);
        }
    }
}
