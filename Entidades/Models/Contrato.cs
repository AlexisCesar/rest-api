namespace Entidades.Models
{
    public abstract class Contrato
    {
        public Guid Id { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime? Termino { get; set; }
        public double Salario { get; set; }
        public string? Cargo { get; set; }
        public Funcionario Funcionario { get; set; }

        public abstract double calcularSalario();
    }
}
