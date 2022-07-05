using System.ComponentModel.DataAnnotations;

namespace Restful_API.ViewModels
{
    public class UpdateContratoPJViewModel
    {
        [Required]
        public string? Cargo { get; set; }
        [Required]
        public double Salario { get; set; }
    }
}