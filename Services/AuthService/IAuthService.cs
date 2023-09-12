using SiPerpusApi.Dto;
using SiPerpusApi.Models;

namespace SiPerpusApi.Services;

public interface IAuthService
{
    Tokens Login(LoginRequest loginRequest);
    
    void Register(RegisterRequest registerRequest);

}