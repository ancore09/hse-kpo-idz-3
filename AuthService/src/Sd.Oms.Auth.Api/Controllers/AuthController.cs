using Microsoft.AspNetCore.Mvc;
using Sd.Oms.Auth.Api.Extensions;
using Sd.Oms.Auth.Api.Requests;
using Sd.Oms.Auth.Core.Interfaces;

namespace Sd.Oms.Auth.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly IAuthenticationService _authenticationService;
    
    public AuthController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegistrationRequest request)
    {
        var dto = request.ToDto();
        
        await _authenticationService.RegisterAsync(dto);
        
        return Ok();
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var dto = request.ToDto();
        
        var responseDto = await _authenticationService.LoginAsync(dto);
        
        return Ok(responseDto);
    }
}