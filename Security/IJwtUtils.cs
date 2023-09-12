using SiPerpusApi.Dto;
using SiPerpusApi.Models;

namespace SiPerpusApi.Security;

public interface IJwtUtils
{
    Tokens GenerateToken(User user);
}