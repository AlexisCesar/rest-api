using Restful_API.ViewModels.Common;
using System.ComponentModel.DataAnnotations;

namespace Restful_API.ViewModels
{
    public class CreateContratoViewModel
    {
        [Required]
        public string? Cargo { get; set; }
        [Required]
        public double Salario { get; set; }
        [Required]
        public TipoContratoEnum TipoContrato { get; set; }
        [Required]
        public Guid FuncionarioId { get; set; }
    }
}
