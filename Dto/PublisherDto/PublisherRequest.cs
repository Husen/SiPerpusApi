using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SiPerpusApi.Dto;

public class PublisherRequest
{
    [JsonPropertyName("namePublisher")]
    [Required(ErrorMessage = "Name Publisher is required")]
    public string NamePublisher { get; set; }
}