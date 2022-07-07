namespace Entidades.Models
{
    public class ContratoCLT : Contrato
    {
        public ContratoCLT()
        {

        }

        public override double calcularBeneficiosSalario(double salarioBruto)
        {
            // Calculo falso apenas para demonstração.
            return salarioBruto * 0.275;
        }

        public override double calcularDescontosSalario(double salarioBruto)
        {
            // Calculo falso apenas para demonstração.
            return salarioBruto * 0.09;
        }
    }
}
