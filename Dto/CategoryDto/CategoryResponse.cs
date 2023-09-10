using System.Text.Json.Serialization;

namespace SiPerpusApi.Dto.CategoryDto;
public class CategoryResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("nameCategory")]
    public string NameCategory { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }
}