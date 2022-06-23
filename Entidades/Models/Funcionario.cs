namespace Entidades.Models
{
    public abstract class Funcionario
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public double Salario { get; set; }

        public abstract double CalcularSalario();
    }
}
