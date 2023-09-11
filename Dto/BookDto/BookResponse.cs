using System.Text.Json.Serialization;
using SiPerpusApi.Models;

namespace SiPerpusApi.Dto;

public class BookResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("codeBook")]
    public string CodeBook { get; set; }
    
    [JsonPropertyName("nameBook")]
    public string NameBook { get; set; }

    [JsonPropertyName("category")]
    public Category Category { get; set; }
    
    [JsonPropertyName("publisher")]
    public Publisher Publisher { get; set; }
    
    [JsonPropertyName("rack")]
    public Rack Rack { get; set; }
    
    [JsonPropertyName("pengarang")]
    public string Pengarang { get; set; }
    
    [JsonPropertyName("isbn")]
    public string ISBN { get; set; }
    
    [JsonPropertyName("pageBook")]
    public int PageBook { get; set; }

    [JsonPropertyName("yearBook")]
    public string YearBook { get; set; }

    [JsonPropertyName("stock")]
    public int Stock { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }
}