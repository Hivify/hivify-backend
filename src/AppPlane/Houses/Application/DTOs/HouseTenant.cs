namespace Houses.Application.DTOs;

public sealed record HouseTenant(
    Guid Id,
    string FirstName,
    string LastName,
    string FullName,
    string Email,
    string PhoneNumber,
    DateTime CreatedAt
);