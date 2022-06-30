using Entidades.Models;
using Entidades.Models.Exceptions;
using Moq;
using Xunit;

namespace Testes.Entidades
{
    public class ContratoTeste
    {
        [Fact]
        public void Impede_Termino_Antes_Do_Inicio()
        {
            var contrato = new Mock<Contrato>();

            contrato.Object.Inicio = System.DateTime.Now.AddDays(5);

            Assert.Throws<DomainValidationException>(() => contrato.Object.Termino = System.DateTime.Now);
        }
    }
}
