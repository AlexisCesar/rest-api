namespace Restful_API.ViewModels 
{
    public class ContratoCLTViewModel 
    {
        public Guid Id { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime? Termino { get; set; }
        public double Salario { get; set; }
        public string? Cargo { get; set; }
        public FuncionarioViewModel Funcionario { get; set; } = null!;
    }
}