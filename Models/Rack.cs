using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiPerpusApi.Models;

public class Rack : Base
{
    [Column("CODE_RACK")]
    [MaxLength(100)]
    public string? CodeRack { get; set; } = string.Empty;
}