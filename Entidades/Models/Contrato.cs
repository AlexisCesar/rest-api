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
                if(value != null)
                {
                    if(value.GetValueOrDefault().Date < Inicio.Date)
                    {
                        throw new DomainValidationException("Data de término não pode ser menor do que início.");
                    }
                }
                _termino = value;
            }
        }
        public double Salario { get; set; }
        public string? Cargo { get; set; }
        public Funcionario Funcionario { get; set; } = null!;

        public abstract double calcularSalario();
    }
}
