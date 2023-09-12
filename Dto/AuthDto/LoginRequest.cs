using System.ComponentModel;
using System.Text.Json.Serialization;

namespace SiPerpusApi.Dto;

public class LoginRequest
{
    [DefaultValue("husen@gmail.com")]
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [DefaultValue("Password123")]
    [JsonPropertyName("password")]
    public string Password { get; set; }
}