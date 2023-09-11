using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SiPerpusApi.Dto;

public class MemberRequest
{
    [JsonPropertyName("fullName")]
    [Required(ErrorMessage = "Fullname is required")]
    public string FullName { get; set; }
    
    [JsonPropertyName("email")]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }
    
    [JsonPropertyName("phoneNumber")]
    [Required(ErrorMessage = "Phone Number is required")]
    public string PhoneNumber { get; set; }
}