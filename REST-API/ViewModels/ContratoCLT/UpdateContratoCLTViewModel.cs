using System.ComponentModel.DataAnnotations;

namespace Restful_API.ViewModels
{
    public class UpdateContratoCLTViewModel
    {
        [Required]
        public string? Cargo { get; set; }
        [Required]
        public double Salario { get; set; }
    }
}