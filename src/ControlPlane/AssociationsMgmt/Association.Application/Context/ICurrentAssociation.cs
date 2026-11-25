using SharedKernel.ValueObjects;

namespace Association.Application.Context;

public interface ICurrentAssociation
{
    AssociationID GetAssociationId();

    Guid GetTierId();

    void EnsureAuthenticated();

    void EnsureAdmin();
}
