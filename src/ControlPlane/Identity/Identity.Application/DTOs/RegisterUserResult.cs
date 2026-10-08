namespace Identity.Application.DTOs;

public sealed record RegisterUserResult(
    Guid UserId,
    bool Succeeded,
    IReadOnlyList<string> Errors);
