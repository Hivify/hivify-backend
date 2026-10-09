using Association.Domain.ValueObjects;
using SharedKernel;
using SharedKernel.ValueObjects;

namespace Association.Domain.Events;

public sealed record AssociationCreatedDomainEvent(AssociationID AssociationId, UserID OwnerId) : IDomainEvent;