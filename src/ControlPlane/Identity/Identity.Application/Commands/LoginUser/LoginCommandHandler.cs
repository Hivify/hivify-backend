
using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using Identity.Application.Contracts;
using Identity.Application.DTOs;

namespace Identity.Application.Commands.LoginUser;

public sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, LoginUserResult>
{
    private readonly IUserIdentityService _identityService;

    public LoginUserCommandHandler(IUserIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<LoginUserResult> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        return await _identityService.LoginAsync(
            command.Email,
            command.Password,
            command.RememberMe,
            cancellationToken);
    }
}
