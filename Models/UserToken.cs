using System.ComponentModel.DataAnnotations.Schema;

namespace SiPerpusApi.Models;

public class UserToken : Base
{
    [Column(name: "REFRESH_TOKEN")]
    public string RefreshToken { get; set; }
    
    [Column(name: "USER_ID")]
    public string UserId { get; set; }
    
    [Column(name: "USER_AGENT")]
    public string UserAgent {get; set;}
    
    [Column(name: "IS_PRIVATE")]
    public int IsPrivate {get; set;}
}