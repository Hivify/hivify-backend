using Complaints.Domain;

namespace Complaints.Application.DTOs;

public sealed record ComplaintListItem(
Guid Id,
Guid AssociationId,
string Title,
string Description,
ComplaintStatus Status,
DateTimeOffset CreatedDate,
string? ImageUrl,
string? AdminComment = null,
string? UserId = null);