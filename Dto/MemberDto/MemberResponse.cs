using System.Text.Json.Serialization;

namespace SiPerpusApi.Dto;
public class MemberResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("fullName")]
    public string FullName { get; set; }
    
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }
}