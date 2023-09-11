using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SiPerpusApi.Dto;

public class LoanDetailDto
{
    [JsonPropertyName("bookId")]
    [Required(ErrorMessage = "Book Id is required")]
    public int BookId { get; set; }
    
    [JsonPropertyName("qty")]
    [Required(ErrorMessage = "Qty Id is required")]
    public int Qty { get; set; }
}