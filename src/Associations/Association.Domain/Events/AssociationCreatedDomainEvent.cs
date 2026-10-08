using Association.Domain.ValueObjects;
using SharedKernel;
using SharedKernel.ValuesObjects;

namespace Association.Domain.Events;

public sealed record AssociationCreatedDomainEvent(AssociationID AssociationId, UserID OwnerId) : IDomainEvent;