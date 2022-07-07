namespace Restful_API.DTOs 
{
    public class ContratoPJDTO 
    {
        public Guid Id { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime? Termino { get; set; }
        public double Salario { get; set; }
        public string? Cargo { get; set; }
        public FuncionarioDTO Funcionario { get; set; } = null!;
    }
}