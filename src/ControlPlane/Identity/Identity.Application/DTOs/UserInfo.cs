namespace Identity.Application.DTOs;

public sealed record UserInfo(
   Guid Id,
    string? FullName,
    string? Email);