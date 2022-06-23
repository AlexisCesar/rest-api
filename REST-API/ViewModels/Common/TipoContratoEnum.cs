using System.Text.Json.Serialization;

namespace Restful_API.ViewModels.Common
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TipoContratoEnum
    {
        CLT = 1,
        PJ = 2,
    }
}
