using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiPerpusApi.Dto;
using SiPerpusApi.Services;

namespace SiPerpusApi.Controllers;

[Route("api/book")]
[ApiExplorerSettings(GroupName = "v1")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpPost, Authorize(Roles = "Petugas, Administrasi")]
    public async Task<IActionResult> CreateBook([Required, FromBody] BookRequest bookRequest)
    {
        BookResponse book = _bookService.CreateBook(bookRequest);
        var response = new ApiResponse<BookResponse>
        {
            Code = 201,
            Message = "Book created successfully",
            Status = "S",
            Data = book
        };
        return Created("api/book", response);
    }
    
    [HttpGet("{id}")]
    [Authorize(Roles = "Petugas, Administrasi")]
    public async Task<IActionResult> GetBookById(int id)
    {
        BookResponse book = _bookService.GetById(id);

        var response = new ApiResponse<BookResponse>()
        {
            Code = 200,
            Message = "OK",
            Status = "OK",
            Data = book
        };
        return Ok(response);
    }
    
    [HttpGet]
    [Authorize(Roles = "Petugas, Administrasi")]
    public async Task<IActionResult> GetAllBooks([FromQuery] RequestPagination requestPagination)
    {
        var bookResponse = _bookService.GetAll(requestPagination);
        return Ok(bookResponse);
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = "Petugas, Administrasi")]
    public async Task<IActionResult> UpdateBookById(int id, [Required, FromBody] BookRequest bookRequest)
    {
        BookResponse book = _bookService.UpdateBook(id, bookRequest);
        
        var response = new ApiResponse<BookResponse>
        {
            Code = 200,
            Message = "Book update successfully",
            Status = "S",
            Data = book
        };
        return Ok(response);
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Petugas, Administrasi")]
    public async Task<IActionResult> DeleteBookById(int id)
    {
        _bookService.DeleteBook(id);
        var response = new ApiResponse<object>
        {
            Code = 200,
            Message = "Book delete successfully",
            Status = "S",
            Data = new object[0]
        };
        return Ok(response);
    }
}