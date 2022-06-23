using Microsoft.AspNetCore.Mvc;
using Entidades.Models;
using Restful_API.ViewModels;

namespace Restful_API.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class FuncionarioController : ControllerBase
    {
        public FuncionarioController()
        {

        }

        [HttpGet]
        [Route("colaboradores")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Funcionario))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetFuncionarios()
        {
            var funcionarios = new List<Funcionario>()
            {
                new FuncionarioCLT()
                {
                    Id = Guid.NewGuid(),
                    Nome = "Funcionário Teste",
                    Salario = 20000
                }
            };

            return funcionarios.Count == 0 ? NoContent() : Ok(funcionarios);
        }

        [HttpPost]
        [Route("colaboradores")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Funcionario))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostFuncionarios([FromBody] CreateFuncionarioViewModel funcionario)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var funcionarioCriado = new FuncionarioCLT()
            {
                Id = Guid.NewGuid(),
                Nome = funcionario.Nome,
                Salario = funcionario.Salario,
            };

            return Created($"api/v1/colaboradores/{funcionarioCriado.Id}", funcionarioCriado);
        }
    }
}
