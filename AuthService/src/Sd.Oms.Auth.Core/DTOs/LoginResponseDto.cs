using Sd.Oms.Auth.Core.Models;

namespace Sd.Oms.Auth.Core.DTOs;

public class LoginResponseDto
{
    public User User { get; set; }
    public string? Token { get; set; }
}