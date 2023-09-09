using System.Text.Json.Serialization;

namespace SiPerpusApi.Dto.ViewModel;

public class ApiResponse<T>
{
    [JsonPropertyName("code")]
    public int Code { get; set; }
    
    [JsonPropertyName("message")]
    public string Message { get; set; }
    
    [JsonPropertyName("status")]
    public string Status { get; set; }
    
    [JsonPropertyName("Data")]
    public T Data { get; set; }
}