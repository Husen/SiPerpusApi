using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiPerpusApi.Models;

public class Book : Base
{
    [Column("CODE_BOOK")]
    [MaxLength(100)]
    public string? CodeBook { get; set; } = string.Empty;
    
    [Column("NAME_BOOK")]
    [MaxLength(100)]
    public string? NameBook { get; set; } = string.Empty;

    [Column("CATEGORY_ID")] 
    public int? CategoryId { get; set; } = null;
    
    [ForeignKey("CategoryId")] 
    public Category Category { get; set; }
    
    [Column("PUBLISHER_ID")] 
    public int? PublisherId { get; set; } = null;
    
    [ForeignKey("PublisherId")] 
    public Publisher Publisher { get; set; }
    
    [Column("RACK_ID")] 
    public int? RackId { get; set; } = null;
    
    [ForeignKey("RackId")] 
    public Rack Rack { get; set; }

    [Column("PENGARANG")] 
    [MaxLength(100)]
    public string? Pengarang { get; set; } = string.Empty;
    
    [Column("ISBN")] 
    [MaxLength(30)]
    public string? ISBN { get; set; } = string.Empty;
    
    [Column("PAGE_BOOK")] 
    public int PageBook { get; set; }

    [Column("YEAR_BOOK")] [MaxLength(4)] 
    public string? YearBook { get; set; } = string.Empty;

    [Column("STOCK")] 
    public int Stock { get; set; } = 0;
}