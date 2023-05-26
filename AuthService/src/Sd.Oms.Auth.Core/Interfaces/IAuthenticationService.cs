using Sd.Oms.Auth.Core.DTOs;
using Sd.Oms.Auth.Core.Models;

namespace Sd.Oms.Auth.Core.Interfaces;

public interface IAuthenticationService
{
    Task<long> RegisterAsync(RegistrationRequestDto dto);
    Task<LoginResponseDto> LoginAsync(LoginRequestDto dto);
}