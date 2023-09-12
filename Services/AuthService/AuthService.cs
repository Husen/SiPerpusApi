using SiPerpusApi.Dto;
using SiPerpusApi.Exceptions;
using SiPerpusApi.Models;
using SiPerpusApi.Repositories;
using SiPerpusApi.Security;

namespace SiPerpusApi.Services;

public class AuthService : IAuthService
{
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Role> _roleRepository;
    private readonly IPersistence _persistence;
    private readonly AppDbContext _dbContext;
    private readonly EncryptUtils _encryptUtils;
    private readonly IJwtUtils _jwtUtils;

    public AuthService(IRepository<User> userRepository, IRepository<Role> roleRepository, IPersistence persistence, AppDbContext dbContext, EncryptUtils encryptUtils, IJwtUtils jwtUtils)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _persistence = persistence;
        _dbContext = dbContext;
        _encryptUtils = encryptUtils;
        _jwtUtils = jwtUtils;
    }

    public Tokens Login(LoginRequest loginRequest)
    {
        var user = _userRepository.FindBy(criteria => criteria.Email.Equals(loginRequest.Email));
        _dbContext.Entry(user)
            .Reference(u => u.Role)
            .Load();
        if (user is null) throw new UnauthorizedException("invalid credential");
        var validate = _encryptUtils.Validate(loginRequest.Password, user.Password);
        
        if (!validate) throw new UnauthorizedException("invalid credential");

        var tokens = _jwtUtils.GenerateToken(user);
        Console.WriteLine(tokens);
        return tokens;
    }

    public void Register(RegisterRequest registerRequest)
    {
        var checkUser = _userRepository.FindBy(criteria => criteria.Email.Equals(registerRequest.Email));

        if (checkUser is not null) throw new ConflictException("Email is exist");
        
        var roleData = _roleRepository.FindBy(criteria => criteria.RoleNameEnum == RoleEnum.Petugas);
        if (roleData is null)
        {
            Role createRole = new Role()
            {
                RoleNameEnum = RoleEnum.Petugas
            };
            roleData = _roleRepository.Save(createRole);
            _persistence.SaveChanges();
        }
        
        var user = new User()
        {
            Name = registerRequest.Name,
            Email = registerRequest.Email,
            Password = _encryptUtils.HashPassword(registerRequest.Password),
            Role = roleData
        };
        var userNew = _userRepository.Save(user);
        _persistence.SaveChanges();
    }
}