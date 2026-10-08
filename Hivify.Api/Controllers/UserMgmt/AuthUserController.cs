using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using Hivify.Api.Controllers.UserMgmt.Requests;
using Identity.Application.Commands.LoginUser;
using Identity.Application.Commands.RegisterUser;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterUserReq request,
        CancellationToken cancellationToken)
    {
        var userId = await _sender.Send(
            new RegisterUserCommand(
                request.Email,
                request.Password,
                request.FullName),
            cancellationToken);

        return Ok(new
        {
            userId
        });
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(
       LoginUserReq request,
       CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new LoginUserCommand(
                request.Email,
                request.Password,
                request.RememberMe
                ),
            cancellationToken);

        if (!result.Succeeded)
        {
            return Unauthorized(result);
        }

        return Ok(result);
    }
}

