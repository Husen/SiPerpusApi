using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiPerpusApi.Dto;
using SiPerpusApi.Services;

namespace SiPerpusApi.Controllers;

[Route("api/loan")]
[ApiExplorerSettings(GroupName = "v1")]
public class LoanController : Controller
{
    private readonly ILoanService _loanService;

    public LoanController(ILoanService loanService)
    {
        _loanService = loanService;
    }

    [HttpPost]
    [Authorize(Roles = "Petugas, Administrasi")]
    public async Task<IActionResult> CreateLoan([Required, FromBody] CreateLoanRequest createLoanRequest)
    {
        var loan = _loanService.CreateLoan(createLoanRequest);
        var response = new ApiResponse<LoanResponse>
        {
            Code = 201,
            Message = "Loan created successfully",
            Status = "S",
            Data = loan
        };
        return Created("api/loan", response);
    }
    
    [HttpGet("{id}")]
    [Authorize(Roles = "Petugas, Administrasi")]
    public async Task<IActionResult> GetLoanById(int id)
    {
        LoanResponse loan = _loanService.GetById(id);

        var response = new ApiResponse<LoanResponse>()
        {
            Code = 200,
            Message = "OK",
            Status = "OK",
            Data = loan
        };
        return Ok(response);
    }
    
    [HttpGet]
    [Authorize(Roles = "Petugas, Administrasi")]
    public async Task<IActionResult> GetAllBooks([FromQuery] RequestPagination requestPagination)
    {
        var bookResponse = _loanService.GetAll(requestPagination);
        return Ok(bookResponse);
    }
    
    [HttpPut("return/{id}")]
    [Authorize(Roles = "Petugas, Administrasi")]
    public async Task<IActionResult> ReturnedLoan(int id, [Required, FromBody] ReturnedLoanRequest returnedLoanRequest)
    {
        LoanResponse loan = _loanService.ReturnedLoan(id, returnedLoanRequest);
        
        var response = new ApiResponse<LoanResponse>
        {
            Code = 200,
            Message = "Returned loan successfully",
            Status = "S",
            Data = loan
        };
        return Ok(response);
    }
}