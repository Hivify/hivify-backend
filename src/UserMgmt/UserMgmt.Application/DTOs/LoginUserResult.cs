namespace UserMgmt.Application.DTOs;

public sealed record LoginUserResult(
    bool Succeeded,
    bool IsLockedOut = false,
    bool RequiresTwoFactor = false);