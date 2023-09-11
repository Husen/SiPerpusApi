using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiPerpusApi.Models;

[Table("LoanDetails")]
public class LoanDetail : Base
{
    [Column("LOAN_ID")]
    public int? LoanId { get; set; }

    [ForeignKey("LoanId")]
    public Loan Loan { get; set; }

    [Column("LOAN_CODE")]
    [MaxLength(100)]
    public string? LoanCode { get; set; }
    
    [Column("BOOK_ID")]
    public int BookId { get; set; }

    [ForeignKey("BookId")]
    public Book Book { get; set; }

    [Column("QTY")]
    public int qty { get; set; }
    
}