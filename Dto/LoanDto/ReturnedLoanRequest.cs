using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SiPerpusApi.Dto;

public class ReturnedLoanRequest
{
    [JsonPropertyName("totalDailyFines")]
    [Required(ErrorMessage = "TotalDailyFines is required")]
    public int TotalDailyFines { get; set; }
    
    [JsonPropertyName("amercement")]
    [Required(ErrorMessage = "Amercement is required")]
    public int Amercement { get; set; }
}