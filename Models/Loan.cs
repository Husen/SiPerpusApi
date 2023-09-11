using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiPerpusApi.Models;

public enum StatusLoan
{
    Loaning = 0,
    Returned = 1
}

[Table("Loans")]
public class Loan : Base
{
    [Column("CODE_LOAN")]
    [MaxLength(100)]
    public string? CodeLoan { get; set; }
    
    [Column("MEMBER_ID")]
    public int MemberId { get; set; }
    
    [ForeignKey("MemberId")]
    public Member Member { get; set; }

    [Column("DURATION")]
    public int Duration { get; set; }
    
    [Column("START_DATE_LOAN")]
    public DateTime StartDateLoan { get; set; }

    [Column("END_DATE_LOAN")]
    public DateTime EndDateLoan { get; set; }

    [Column("STATUS_LOAN")] 
    public StatusLoan StatusLoan { get; set; } = StatusLoan.Loaning;

    [Column("TOTAL_DAILY_FINES")]
    public int? TotalDailyFines { get; set; }
    
    [Column("AMERCEMENT")]
    public int? Amercement { get; set; }
}