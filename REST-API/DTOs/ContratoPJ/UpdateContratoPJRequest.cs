using System.ComponentModel.DataAnnotations;

namespace Restful_API.DTOs
{
    public class UpdateContratoPJRequest
    {
        [Required]
        public string? Cargo { get; set; }
        [Required]
        public decimal SalarioBruto { get; set; }
    }
}