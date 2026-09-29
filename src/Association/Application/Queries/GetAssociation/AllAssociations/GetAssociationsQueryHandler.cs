using Association.Application.Contracts;
using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.Messaging;

namespace Association.Application.Queries.GetAssociation.AllAssociations;

public sealed class GetAssociationsQueryHandler
    : IQueryHandler<
        GetAssociationsQuery,
        IReadOnlyList<AssociationListItem>>
{
    private readonly IAssociationRepo _associationRepository;

    public GetAssociationsQueryHandler(
        IAssociationRepo associationRepository)
    {
        _associationRepository = associationRepository;
    }

    public async Task<IReadOnlyList<AssociationListItem>> Handle(
       GetAssociationsQuery query,
       CancellationToken cancellationToken)
    {
        var associations =
            await _associationRepository.GetAllAsync(
                cancellationToken);

        return associations
            .Select(association =>
                new AssociationListItem
                {
                    Id = association.Id.Value,
                    Name = association.Name.Value,

                    StaffMembers = association.StaffMembers
                        .Where(member => member.DeletedAt == null)
                        .Select(member => new StaffMemberItem(
                            member.Id.Value,
                            member.FullName.Value,
                            member.Email.Value,
                            member.Role))
                        .ToList()
                })
            .ToList();
    }
}



