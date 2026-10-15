using Hivify.Api.Controllers.ControlPlane.Identity.Requests;
using Identity.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(IUserIdentityService identitySerivce) : ControllerBase
{
    private readonly IUserIdentityService _identitySerivce = identitySerivce;

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterUserReq request,
        CancellationToken ct)
    {
        var userId = await _identitySerivce.RegisterAsync(
            request.Email,
            request.Password,
            request.FullName,
            ct);

        return Ok(new
        {
            userId
        });
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(
       LoginUserReq request,
       CancellationToken ct)
    {
        var result = await _identitySerivce.LoginAsync(
            request.Email,
            request.Password,
            request.RememberMe,
            ct);

        if (!result.Succeeded)
        {
            return Unauthorized(result);
        }

        return Ok(result);
    }
}

