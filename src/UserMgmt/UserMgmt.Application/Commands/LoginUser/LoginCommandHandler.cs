
using BuildingBlocks.ApplicationPorts.Messaging;
using UserMgmt.Application.Contracts;
using UserMgmt.Application.DTOs;

namespace UserMgmt.Application.Commands.LoginUser;

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
