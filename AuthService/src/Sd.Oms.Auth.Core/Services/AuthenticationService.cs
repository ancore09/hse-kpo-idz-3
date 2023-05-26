using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Sd.Oms.Auth.Core.DTOs;
using Sd.Oms.Auth.Core.Entities;
using Sd.Oms.Auth.Core.Exceptions;
using Sd.Oms.Auth.Core.Extensions;
using Sd.Oms.Auth.Core.Interfaces;
using Sd.Oms.Auth.Core.Models;

namespace Sd.Oms.Auth.Core.Services;

public class AuthenticationService: IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly TokenValidationParameters _validationParameters;

    public AuthenticationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _validationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = "AuthService",
            ValidateAudience = true,
            ValidAudience = "OmsServices",
            ValidateLifetime = true,
            IssuerSigningKeyResolver = (string _, SecurityToken _, string _,
                TokenValidationParameters _) => new List<SecurityKey>()
            {
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("jwt-giga-mega-sigma-super-secret-key"))
            },
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero
        };
    }
    
    public async Task<long> RegisterAsync(RegistrationRequestDto dto)
    {
        var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
        
        if (existingUser != null)
        {
            throw new UserAlreadyExistsException();
        }
        
        var hash = HashPassword(dto.Password);
        
        var user = new User
        {
            Email = dto.Email,
            Username = dto.Login,
            PasswordHash = hash,
            Role = dto.Role
        };
        
        return await _userRepository.InsertAsync(UserEntity.FromModel(user));
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
    {
        var entity = await _userRepository.GetByEmailAsync(dto.Email);
        if (entity == null)
        {
            throw new NotFoundException("User not found");
        }
        
        var hash = HashPassword(dto.Password);
        
        if (entity.PasswordHash != hash)
        {
            throw new InvalidPasswordException();
        }
        
        return new LoginResponseDto()
        {
            User = entity.ToModel(),
            Token = GenerateToken(10, new List<Claim>()
            {
                new(ClaimTypes.Role, entity.Role),
                new(ClaimTypes.NameIdentifier, entity.Id.ToString())
            })
        };
    }
    
    private string GenerateToken(int lifetimeInMinutes, IEnumerable<Claim> claims)
    {
        var jwt = new JwtSecurityToken(
            issuer: _validationParameters.ValidIssuer,
            audience: _validationParameters.ValidAudience,
            claims: claims,
            notBefore: DateTime.Now.ToUniversalTime(),
            expires: DateTime.Now.AddMinutes(lifetimeInMinutes).ToUniversalTime(),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jwt-giga-mega-sigma-super-secret-key")),
                SecurityAlgorithms.HmacSha256
            )
        );     
           
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
    
    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}