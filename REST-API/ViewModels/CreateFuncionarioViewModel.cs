using Restful_API.ViewModels.Common;
using System.ComponentModel.DataAnnotations;

namespace Restful_API.ViewModels
{
    public class CreateFuncionarioViewModel
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public double Salario { get; set; }
        [Required]
        public TipoContratoEnum TipoContrato { get; set; }
    }
}
