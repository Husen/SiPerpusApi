using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiPerpusApi.Models;

public class Base
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID")]
    public int Id { get; set; }

    [Column("CREATED_AT", TypeName = "timestamp")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("UPDATED_AT", TypeName = "timestamp")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
