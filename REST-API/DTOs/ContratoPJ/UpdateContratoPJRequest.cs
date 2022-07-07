using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Restful_API.DTOs
{
    public class UpdateContratoPJRequest
    {
        [Required]
        public string? Cargo { get; set; }
        [Required]
        public decimal SalarioBruto { get; set; }
        [JsonIgnore]
        public DateTime? Termino { get; set; }
    }
}