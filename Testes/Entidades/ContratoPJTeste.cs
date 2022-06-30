using Entidades.Models;
using Xunit;

namespace Testes.Entidades
{
    public class ContratoPJTeste
    {
        [Fact]
        public void Pode_Ser_Instanciado()
        {
            var contrato = new ContratoPJ();

            Assert.NotNull(contrato);
        }
    }
}
