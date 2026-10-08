using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using Identity.Application.DTOs;

namespace Identity.Application.Commands.LoginUser;

public sealed record LoginUserCommand(
    string Email,
    string Password,
    bool RememberMe) : ICommand<LoginUserResult>;