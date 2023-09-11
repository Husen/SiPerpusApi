using System.Text.Json.Serialization;

namespace SiPerpusApi.Dto;

public class RequestPagination
{
    [JsonPropertyName("page")]
    public int? Page { get; set; } = 1;
    
    [JsonPropertyName("limit")]
    public int? Limit { get; set; } = 10;
    
    [JsonPropertyName("searchQuery")]
    public string? SearchQuery { get; set; } = "";
    
    
    /// <summary>
    /// sortBy = id , field etc
    /// </summary>
    [JsonPropertyName("sortBy")]
    public string? SortBy { get; set; } = "";
    
    /// <summary>
    /// sortDirection = asc || desc
    /// </summary>
    
    [JsonPropertyName("sortDirection")]
    public string? SortDirection { get; set; } = "";
}