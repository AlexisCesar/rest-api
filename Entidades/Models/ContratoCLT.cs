namespace Entidades.Models
{
    public class ContratoCLT : Contrato
    {
        public ContratoCLT()
        {

        }

        public override decimal calcularBeneficiosSalario(decimal salarioBruto)
        {
            // Calculo falso apenas para demonstração.
            return salarioBruto * (decimal) 0.275;
        }

        public override decimal calcularDescontosSalario(decimal salarioBruto)
        {
            // Calculo falso apenas para demonstração.
            return salarioBruto * (decimal) 0.09;
        }
    }
}
