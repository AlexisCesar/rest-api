using System.ComponentModel.DataAnnotations;

namespace Restful_API.DTOs
{
    public class CreateContratoPJRequest
    {
        [Required]
        public string? Cargo { get; set; }
        [Required]
        public double Salario { get; set; }
        [Required]
        public Guid FuncionarioId { get; set; }
    }
}