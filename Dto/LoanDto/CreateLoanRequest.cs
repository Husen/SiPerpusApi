using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SiPerpusApi.Dto;

public class CreateLoanRequest
{
    [JsonPropertyName("codeLoan")]
    [Required(ErrorMessage = "Code Loan is required")]
    public string CodeLoan { get; set; }

    [JsonPropertyName("memberId")]
    [Required(ErrorMessage = "Member Id is required")]
    public int MemberId { get; set; }

    [JsonPropertyName("duration")]
    [Required(ErrorMessage = "Duration is required")]
    public int Duration { get; set; }

    [JsonPropertyName("startDateLoan")]
    [Required(ErrorMessage = "Start Date Loan is required")]
    public string StartDateLoan { get; set; } // 2023-09-11 YYYY-MM-DD

    [JsonPropertyName("endDateLoan")]
    [Required(ErrorMessage = "End Date Loan is required")]
    public string EndDateLoan { get; set; }

    [JsonPropertyName("loanDetails")]
    public List<LoanDetailDto> LoanDetailsDto { get; set; }

}
