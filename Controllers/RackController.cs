using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SiPerpusApi.Dto.RackDto;
using SiPerpusApi.Dto.ViewModel;
using SiPerpusApi.Services;

namespace SiPerpusApi.Controllers;

[Route("api/rack")]
[ApiExplorerSettings(GroupName = "v1")]
public class RackController : ControllerBase
{
    private readonly IRackService _rackService;

    public RackController(IRackService rackService)
    {
        _rackService = rackService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateRack([Required, FromBody] RackRequest rackRequest)
    {
        RackResponse rack = _rackService.CreateRack(rackRequest);
        var response = new ApiResponse<RackResponse>
        {
            Code = 201,
            Message = "Rack created successfully",
            Status = "S",
            Data = rack
        };
        return Created("api/rack", response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRackById(int id)
    {
        RackResponse rack = _rackService.GetById(id);

        var response = new ApiResponse<RackResponse>()
        {
            Code = 200,
            Message = "OK",
            Status = "OK",
            Data = rack
        };
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRacks([FromQuery] RequestPagination requestPagination)
    {
        var responseRacks = _rackService.GetAll(requestPagination);
        return Ok(responseRacks);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRackById(int id, [Required, FromBody] RackRequest rackRequest)
    {
        RackResponse rack = _rackService.UpdateRack(id, rackRequest);
        
        var response = new ApiResponse<RackResponse>
        {
            Code = 200,
            Message = "Rack update successfully",
            Status = "S",
            Data = rack
        };
        return Ok(response);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRackById(int id)
    {
        _rackService.DeleteRack(id);
        var response = new ApiResponse<object>
        {
            Code = 200,
            Message = "Rack delete successfully",
            Status = "S",
            Data = new object[0]
        };
        return Ok(response);
    }
}