using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiPerpusApi.Models;

public enum RoleEnum
{
    Administrasi,
    Petugas,
}

[Table("ROLES")]
public class Role
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID")]
    public int Id { get; set; }

    [Column("RoleName")]
    public RoleEnum RoleNameEnum { get; set; } = RoleEnum.Petugas;

    [NotMapped]
    public string RoleName
    {
        get { return RoleNameEnum.ToString(); }
        set { RoleNameEnum = (RoleEnum)Enum.Parse(typeof(RoleEnum), value, true); }
    }
}