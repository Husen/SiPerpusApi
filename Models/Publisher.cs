using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiPerpusApi.Models;

public class Publisher : Base
{
    [Column("NAME_PUBLISHER")]
    [MaxLength(100)]
    public string? NamePublisher { get; set; } = string.Empty;
}