using System.Text.Json.Serialization;

namespace SiPerpusApi.Dto;

public class Tokens
{
    [JsonPropertyName("accessToken")]
    public string AccessToken { get; set; }
    
    [JsonPropertyName("refreshToken")]
    public string RefreshToken { get; set; }
    
    [JsonPropertyName("expiredAt")]
    public long ExpiredAt { get; set; }
}