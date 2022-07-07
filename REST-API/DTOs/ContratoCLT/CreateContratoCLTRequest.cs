using System.ComponentModel.DataAnnotations;

namespace Restful_API.DTOs
{
    public class CreateContratoCLTRequest
    {
        [Required]
        public string? Cargo { get; set; }
        [Required]
        public decimal SalarioBruto { get; set; }
        [Required]
        public Guid FuncionarioId { get; set; }
    }
}