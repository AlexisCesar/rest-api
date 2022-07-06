using Entidades.Models.Exceptions;

namespace Entidades.Models
{
    public abstract class Contrato
    {
        public Guid Id { get; set; }
        public DateTime Inicio { get; set; }
        private DateTime? _termino { get; set; }
        public DateTime? Termino
        {
            get => _termino;
            set
            {
                if (value != null)
                {
                    if (value.GetValueOrDefault().Date < Inicio.Date)
                    {
                        throw new DomainValidationException("Data de término não pode ser menor do que início.");
                    }
                }
                _termino = value;
            }
        }
        private double _salario { get; set; }
        public double Salario { get => calcularSalario(_salario); set { _salario = value; } }
        public string? Cargo { get; set; }
        public Funcionario Funcionario { get; set; } = null!;

        public double calcularSalario(double salarioBruto)
        {
            return salarioBruto - calcularDescontosSalario(salarioBruto) + calcularBeneficiosSalario(salarioBruto);
        }

        public abstract double calcularDescontosSalario(double salarioBruto);

        public abstract double calcularBeneficiosSalario(double salarioBruto);
    }
}
