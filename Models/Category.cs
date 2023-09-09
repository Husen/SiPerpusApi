using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiPerpusApi.Models;

public class Category : Base
{
    [Column("NAME_CATEGORY")]
    [MaxLength(100)]
    public string? NameCategory { get; set; } = string.Empty;
}