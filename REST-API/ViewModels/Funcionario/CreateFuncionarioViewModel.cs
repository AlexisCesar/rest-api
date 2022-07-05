using System.ComponentModel.DataAnnotations;

namespace Restful_API.ViewModels
{
    public class CreateFuncionarioViewModel
    {
        [Required]
        public string Nome { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
    }
}
