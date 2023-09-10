using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SiPerpusApi.Dto.RackDto;

public class RackRequest
{
    [JsonPropertyName("codeRack")]
    [Required(ErrorMessage = "Code Rack is required")]
    public string CodeRack { get; set; }
}