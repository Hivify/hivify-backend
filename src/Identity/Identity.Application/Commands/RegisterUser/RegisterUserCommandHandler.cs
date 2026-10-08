using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using Identity.Application.Contracts;

namespace Identity.Application.Commands.RegisterUser;

public sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IUserIdentityService _identityService;

    public RegisterUserCommandHandler(
        IUserIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Guid> Handle(
        RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _identityService.RegisterAsync(
            command.Email,
            command.Password,
            command.FullName,
            cancellationToken);

        if (!result.Succeeded)
        {
            throw new InvalidOperationException(
                string.Join(", ", result.Errors));
        }
        return result.UserId;
    }
}