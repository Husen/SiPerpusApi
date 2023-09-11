using System.Text.Json.Serialization;

namespace SiPerpusApi.Dto;

public class ApiPaginationResponse<T>
{
    [JsonPropertyName("page")]
    public int Page { get; set; }
    
    [JsonPropertyName("limit")]
    public int Limit { get; set; }
    
    [JsonPropertyName("totalRows")]
    public int TotalRows { get; set; }
    
    [JsonPropertyName("totalPage")]
    public int TotalPage { get; set; }
    
    [JsonPropertyName("data")]
    public T Data { get; set; }
}