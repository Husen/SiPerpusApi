using System.Text.Json.Serialization;

namespace SiPerpusApi.Dto.PublisherDto;

public class PublisherResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("namePublisher")]
    public string NamePublisher { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }
}