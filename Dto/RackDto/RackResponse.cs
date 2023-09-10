using System.Text.Json.Serialization;

namespace SiPerpusApi.Dto.RackDto;

public class RackResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("codeRack")]
    public string CodeRack { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }
}