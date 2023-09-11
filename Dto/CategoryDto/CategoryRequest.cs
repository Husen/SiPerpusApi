using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SiPerpusApi.Dto;

public class CategoryRequest
{
    [JsonPropertyName("nameCategory")]
    [Required(ErrorMessage = "Name Category is required")]
    public string NameCategory { get; set; }
}