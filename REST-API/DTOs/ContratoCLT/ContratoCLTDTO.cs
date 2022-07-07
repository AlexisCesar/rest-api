namespace Restful_API.DTOs 
{
    public class ContratoCLTDTO 
    {
        public Guid Id { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime? Termino { get; set; }
        public decimal SalarioBruto { get; set; }
        public decimal SalarioLiquido { get; set; }
        public string? Cargo { get; set; }
        public Guid FuncionarioId { get; set; }
    }
}