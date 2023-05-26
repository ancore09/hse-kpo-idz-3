namespace Sd.Oms.Auth.Core.DTOs;

public class RegistrationRequestDto
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Login { get; set; }
    public string? Role { get; set; }
}