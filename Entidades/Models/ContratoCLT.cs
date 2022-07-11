namespace Entidades.Models
{
    public class ContratoCLT : Contrato
    {
        public ContratoCLT(DateTime inicio, DateTime? termino, decimal salarioBruto, string cargo, Funcionario funcionario) : base(inicio, termino, salarioBruto, cargo, funcionario)
        {
            
        }

        private ContratoCLT() {}

        public override decimal CalcularBeneficiosSalario(decimal salarioBruto)
        {
            // Calculo falso apenas para demonstração.
            return salarioBruto * (decimal) 0.275;
        }

        public override decimal CalcularDescontosSalario(decimal salarioBruto)
        {
            // Calculo falso apenas para demonstração.
            return salarioBruto * (decimal) 0.09;
        }
    }
}
