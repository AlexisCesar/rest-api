namespace Entidades.Models
{
    public class ContratoPJ : Contrato
    {
        public ContratoPJ(DateTime inicio, DateTime? termino, decimal salarioBruto, string cargo, Guid funcionarioId) : base(inicio, termino, salarioBruto, cargo, funcionarioId)
        {

        }

        private ContratoPJ() {}

        public override decimal CalcularBeneficiosSalario(decimal salarioBruto)
        {
            // Calculo falso apenas para demonstração.
            return 0;
        }

        public override decimal CalcularDescontosSalario(decimal salarioBruto)
        {
            // Calculo falso apenas para demonstração.
            return salarioBruto * (decimal) 0.0466;
        }
    }
}
