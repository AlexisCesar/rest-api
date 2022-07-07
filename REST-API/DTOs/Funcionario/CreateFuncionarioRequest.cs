using System.ComponentModel.DataAnnotations;

namespace Restful_API.DTOs
{
    public class CreateFuncionarioRequest
    {
        [Required]
        public string Nome { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
    }
}
