using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiPerpusApi.Models;

public class Member : Base
{
    [Column("FULL_NAME")]
    [MaxLength(100)]
    public string? FullName { get; set; } = string.Empty;
    
    [Column("EMAIL")]
    [MaxLength(100)]
    public string? Email { get; set; } = string.Empty;
    
    [Column("PHONE_NUMBER")]
    [MaxLength(20)]
    public string? PhoneNumber { get; set; } = string.Empty;
}