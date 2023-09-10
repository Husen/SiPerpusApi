using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SiPerpusApi.Dto.RackDto;

public class BookRequest
{
    [JsonPropertyName("codeBook")]
    [Required(ErrorMessage = "Code Book is required")]
    public string CodeBook { get; set; }
    
    [JsonPropertyName("nameBook")]
    [Required(ErrorMessage = "Name Book is required")]
    public string NameBook { get; set; }

    [JsonPropertyName("categoryId")]
    [Required(ErrorMessage = "Category Id is required")]
    public int CategoryId { get; set; }
    
    [JsonPropertyName("publisherId")]
    [Required(ErrorMessage = "Publisher Id is required")]
    public int PublisherId { get; set; }
    
    [JsonPropertyName("rackId")]
    [Required(ErrorMessage = "Rack Id is required")]
    public int RackId { get; set; }
    
    [JsonPropertyName("pengarang")]
    [Required(ErrorMessage = "Pengarang Book is required")]
    public string Pengarang { get; set; }
    
    [JsonPropertyName("isbn")]
    [Required(ErrorMessage = "ISBN is required")]
    public string ISBN { get; set; }
    
    [JsonPropertyName("pageBook")]
    [Required(ErrorMessage = "Page Book is required")]
    public int PageBook { get; set; }

    [JsonPropertyName("yearBook")]
    [Required(ErrorMessage = "Year Book is required")]
    public string YearBook { get; set; }

    [JsonPropertyName("stock")]
    [Required(ErrorMessage = "Stock is required")]
    public int Stock { get; set; }

}