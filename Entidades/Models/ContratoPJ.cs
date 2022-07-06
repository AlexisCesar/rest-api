namespace Entidades.Models
{
    public class ContratoPJ : Contrato
    {
        public ContratoPJ() : base()
        {

        }

        public override double calcularBeneficiosSalario(double salarioBruto)
        {
            // Calculo falso apenas para demonstração.
            return 0;
        }

        public override double calcularDescontosSalario(double salarioBruto)
        {
            // Calculo falso apenas para demonstração.
            return salarioBruto * 0.0466;
        }
    }
}
