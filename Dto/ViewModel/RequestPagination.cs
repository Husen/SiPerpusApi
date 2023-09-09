using System.Text.Json.Serialization;

namespace SiPerpusApi.Dto.ViewModel;

public class RequestPagination
{
    [JsonPropertyName("page")]
    public int? Page { get; set; } = 1;
    
    [JsonPropertyName("limit")]
    public int? Limit { get; set; } = 10;
    
    [JsonPropertyName("searchQuery")]
    public string? SearchQuery { get; set; } = "";
    
    [JsonPropertyName("sortBy")]
    public string? SortBy { get; set; } = "";
    
    [JsonPropertyName("sortDirection")]
    public string? SortDirection { get; set; } = "";
}