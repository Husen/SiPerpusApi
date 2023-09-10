using System.Text.Json.Serialization;

namespace SiPerpusApi.Dto.ViewModel;

public class ErrorResponse
{
    [JsonPropertyName("code")]
    public string Code {get; set;}
    [JsonPropertyName("status")]
    public string Status { get; set;}
    [JsonPropertyName("message")]
    public string Message {get; set;}
    public List<string> Data { get; set;} = new List<string>();
}