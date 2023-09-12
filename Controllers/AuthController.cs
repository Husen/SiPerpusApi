using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiPerpusApi.Dto;
using SiPerpusApi.Exceptions;
using SiPerpusApi.Models;
using SiPerpusApi.Services;

namespace SiPerpusApi.Controllers;

[Route("api/auth")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterNewCustomer([FromBody] RegisterRequest registerRequest)
    {
        _authService.Register(registerRequest);
        var response = new ApiResponse<object>
        {
            Code = 201,
            Message = "Register successfully",
            Status = "S",
            Data = new object[0]
        };
        return Created("/api/auth/register", response);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var clientId = HttpContext.Request.Headers["X-Client-Id"];
        if (clientId.ToString() is "") throw new BadRequestException("X-Client-Id header required");
        var isClientIdContainPrivatePreffix = clientId.ToString().Split("-")[0];
        var isInPrivateMode = (isClientIdContainPrivatePreffix == "private") ? 1 : 0;
        var tokens = _authService.Login(loginRequest);
        var response = new ApiResponse<Tokens>()
        {
            Code = 200,
            Message = "Login Success",
            Status = "success",
            Data = tokens
        };

        return Ok(response);
    }
}