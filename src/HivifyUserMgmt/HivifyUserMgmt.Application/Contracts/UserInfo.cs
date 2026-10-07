namespace HivifyUserMgmt.Application.Contracts;

public sealed record UserInfo(
    Guid Id,
    string FullName,
    string Email);