using BuildingBlocks.ApplicationPorts.Messaging;
using Identity.Application.DTOs;

namespace Identity.Application.Commands.LoginUser;

public sealed record LoginUserCommand(
    string Email,
    string Password,
    bool RememberMe) : ICommand<LoginUserResult>;