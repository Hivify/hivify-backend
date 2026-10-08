using BuildingBlocks.ApplicationPorts.Contracts.Messaging;

namespace Identity.Application.Commands.RegisterUser;

public sealed record RegisterUserCommand(
    string Email,
    string Password,
    string FullName) : ICommand<Guid>;
