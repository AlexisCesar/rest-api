using Entidades.Models;
using Xunit;

namespace Testes.Entidades
{
    public class ContratoCLTTeste
    {
        [Fact]
        public void Pode_Ser_Instanciado()
        {
            var contrato = new ContratoCLT();

            Assert.NotNull(contrato);
        }
    }
}
