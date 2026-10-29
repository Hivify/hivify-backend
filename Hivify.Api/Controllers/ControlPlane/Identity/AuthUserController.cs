using Hivify.Api.Controllers.ControlPlane.Identity.Requests;
using Identity.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(IUserIdentityService identitySerivce) : ControllerBase
{
    private readonly IUserIdentityService _identitySerivce = identitySerivce;


    /// <summary>
    ///  register a new user
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserReq request, CancellationToken ct)
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



    /// <summary>
    ///         confirms the email address of a user by validating the provided token.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(
    [FromBody] ConfirmEmailRequest request,
    CancellationToken cancellationToken)
    {
        var result = await _identitySerivce.ConfirmEmailAsync(
            request.UserId,
            request.Token,
            cancellationToken);

        if (!result.Succeeded)
        {
            return BadRequest(new
            {
                message = "Email confirmation failed.",
                errors = result.Errors
            });
        }

        return Ok(new
        {
            message = "Email confirmed successfully."
        });
    }


    /// <summary>
    ///   Login Endpoint
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>

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

