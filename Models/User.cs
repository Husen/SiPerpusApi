using System.ComponentModel.DataAnnotations.Schema;

namespace SiPerpusApi.Models;

[Table("M_USERS")]
public class User : Base
{
    [Column("NAME")] 
    public string Name { get; set; } = string.Empty;
    
    [Column("EMAIL")] 
    public string Email { get; set; } = string.Empty;
    
    [Column("PASSWORD")] 
    public string Password { get; set; } = string.Empty;
    
    [Column("ROLE_ID")] 
    public int RoleId { get; set; }
    
    [ForeignKey("RoleId")]
    public Role Role { get; set; }
}