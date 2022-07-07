namespace Entidades.Models
{
    public class ContratoPJ : Contrato
    {
        public ContratoPJ()
        {

        }

        public override decimal calcularBeneficiosSalario(decimal salarioBruto)
        {
            // Calculo falso apenas para demonstração.
            return 0;
        }

        public override decimal calcularDescontosSalario(decimal salarioBruto)
        {
            // Calculo falso apenas para demonstração.
            return salarioBruto * (decimal) 0.0466;
        }
    }
}
