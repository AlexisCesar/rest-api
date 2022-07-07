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

        [Theory]
        [InlineData(100)]
        [InlineData(10)]
        [InlineData(20534)]
        [InlineData(6000)]
        public void Calcula_Beneficio_Corretamente(decimal salarioBruto)
        {
            var contrato = new ContratoCLT();
            var beneficioEsperado = salarioBruto * (decimal) 0.275;

            Assert.Equal(beneficioEsperado, contrato.calcularBeneficiosSalario(salarioBruto));
        }
    }
}