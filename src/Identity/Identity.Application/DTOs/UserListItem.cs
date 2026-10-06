namespace Identity.Application.DTOs;

public sealed record UserListItem(
    Guid Id,
    string? FullName,
    string? UserName,
    string? Email,
    string? PhoneNumber,
    bool EmailConfirmed);
