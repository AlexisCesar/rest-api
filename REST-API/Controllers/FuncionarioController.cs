using Microsoft.AspNetCore.Mvc;
using Entidades.Models;

namespace Restful_API.Controllers
{
    [ApiController]
    [Route("[controller]/v1")]
    public class FuncionarioController : ControllerBase
    {
        public FuncionarioController()
        {

        }

        [HttpGet(Name = "GetFuncionarios")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Funcionario))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Funcionario))]
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

    }
}
