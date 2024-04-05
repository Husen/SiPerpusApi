using System.ComponentModel;
using System.Text.Json.Serialization;

namespace SiPerpusApi.Dto;

public class RegisterRequest
{
    [DefaultValue("Nama Anda")]
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [DefaultValue("anda@gmail.com")]
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [DefaultValue("Qwerty111")]
    [JsonPropertyName("password")]
    public string Password { get; set; }
}
