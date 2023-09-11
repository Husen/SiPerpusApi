using System.Text.Json.Serialization;
using SiPerpusApi.Models;

namespace SiPerpusApi.Dto;

public class LoanResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("codeLoan")]
    public string CodeLoan { get; set; }

    [JsonPropertyName("memberId")]
    public int MemberId { get; set; }

    [JsonPropertyName("duration")]
    public int Duration { get; set; }

    [JsonPropertyName("startDateLoan")]
    public DateOnly StartDateLoan { get; set; }

    [JsonPropertyName("endDateLoan")]
    public DateOnly EndDateLoan { get; set; }
    
    [JsonPropertyName("statusLoan")] 
    public StatusLoan StatusLoan { get; set; }

    [JsonPropertyName("totalDailyFines")]
    public int TotalDailyFines { get; set; }
    
    [JsonPropertyName("amercement")]
    public int Amercement { get; set; }
    
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("loanDetails")]
    public List<LoanDetail> LoanDetails { get; set; }
}