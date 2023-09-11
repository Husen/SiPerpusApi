using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SiPerpusApi.Dto;
using SiPerpusApi.Services;

namespace SiPerpusApi.Controllers;

[Route("api/publisher")]
[ApiExplorerSettings(GroupName = "v1")]
public class PublisherController : ControllerBase
{
    private readonly IPublisherService _publisherService;

    public PublisherController(IPublisherService publisherService)
    {
        _publisherService = publisherService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePublisher([Required, FromBody] PublisherRequest publisherRequest)
    {
        PublisherResponse publisher = _publisherService.CreatePublisher(publisherRequest);
        var response = new ApiResponse<PublisherResponse>
        {
            Code = 201,
            Message = "Publisher created successfully",
            Status = "S",
            Data = publisher
        };
        return Created("api/publisher", response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPublisherById(int id)
    {
        PublisherResponse publisher = _publisherService.GetById(id);

        var response = new ApiResponse<PublisherResponse>()
        {
            Code = 200,
            Message = "OK",
            Status = "OK",
            Data = publisher
        };
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPublishers([FromQuery] RequestPagination requestPagination)
    {
        var responsePublishers = _publisherService.GetAll(requestPagination);
        return Ok(responsePublishers);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePublisherById(int id, [Required, FromBody] PublisherRequest publisherRequest)
    {
        PublisherResponse publisher = _publisherService.UpdatePublisher(id, publisherRequest);
        
        var response = new ApiResponse<PublisherResponse>
        {
            Code = 200,
            Message = "Publisher update successfully",
            Status = "S",
            Data = publisher
        };
        return Ok(response);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePublisherById(int id)
    {
        _publisherService.DeletePublisher(id);
        var response = new ApiResponse<object>
        {
            Code = 200,
            Message = "Publisher delete successfully",
            Status = "S",
            Data = new object[0]
        };
        return Ok(response);
    }
}