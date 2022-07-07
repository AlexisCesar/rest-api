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
        public decimal SalarioLiquido { get; private set; }
        private decimal _salarioBruto { get; set; }
        public decimal SalarioBruto
        {
            get => _salarioBruto;
            set
            {
                _salarioBruto = value;
                SalarioLiquido = calcularSalarioLiquido(value);
            }
        }
        public string? Cargo { get; set; }
        public Funcionario Funcionario { get; set; } = null!;

        public decimal calcularSalarioLiquido(decimal salarioBruto)
        {
            return salarioBruto - calcularDescontosSalario(salarioBruto) + calcularBeneficiosSalario(salarioBruto);
        }

        public abstract decimal calcularDescontosSalario(decimal salarioBruto);

        public abstract decimal calcularBeneficiosSalario(decimal salarioBruto);
    }
}
