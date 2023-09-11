using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SiPerpusApi.Dto;
using SiPerpusApi.Services;

namespace SiPerpusApi.Controllers;

[Route("api/member")]
[ApiExplorerSettings(GroupName = "v1")]
public class MemberController : ControllerBase
{
    private readonly IMemberService _memberService;

    public MemberController(IMemberService memberService)
    {
        _memberService = memberService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateMember([Required, FromBody] MemberRequest memberRequest)
    {
        MemberResponse member = _memberService.CreateMember(memberRequest);
        var response = new ApiResponse<MemberResponse>
        {
            Code = 201,
            Message = "Member created successfully",
            Status = "S",
            Data = member
        };
        return Created("api/member", response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMemberById(int id)
    {
        MemberResponse member = _memberService.GetById(id);

        var response = new ApiResponse<MemberResponse>()
        {
            Code = 200,
            Message = "OK",
            Status = "OK",
            Data = member
        };
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories([FromQuery] RequestPagination requestPagination)
    {
        var responseCategories = _memberService.GetAll(requestPagination);
        return Ok(responseCategories);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMemberById(int id, [Required, FromBody] MemberRequest memberRequest)
    {
        MemberResponse member = _memberService.UpdateMember(id, memberRequest);
        
        var response = new ApiResponse<MemberResponse>
        {
            Code = 200,
            Message = "Member update successfully",
            Status = "S",
            Data = member
        };
        return Ok(response);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMemberById(int id)
    {
        _memberService.DeleteMember(id);
        var response = new ApiResponse<object>
        {
            Code = 200,
            Message = "Member delete successfully",
            Status = "S",
            Data = new object[0]
        };
        return Ok(response);
    }
}