using Association.Application.Contracts;
using Association.Application.DTOs;
using Association.Application.Queries.GetAssociation.SingleAssociation;
using Association.Domain.Associations;
using BuildingBlocks.ApplicationPorts.Messaging;

public sealed class GetAssociationQueryHandler
    : IQueryHandler<GetAssociationQuery, AssociationListItem>
{
    private readonly IAssociationRepo _associationRepository;

    public GetAssociationQueryHandler(
        IAssociationRepo associationRepository)
    {
        _associationRepository = associationRepository;
    }

    public async Task<AssociationListItem> Handle(
        GetAssociationQuery query,
        CancellationToken cancellationToken)
    {
        var AssociationEntity =
            await _associationRepository.GetByIdAsync(
                new AssociationID(query.AssociationId),
                cancellationToken);

        if (AssociationEntity is null)
            throw new InvalidOperationException(
                "AssociationEntity was not found.");

        return new AssociationListItem
        {
            Id = AssociationEntity.Id.Value,
            Name = AssociationEntity.Name.Value,

            StaffMembers = AssociationEntity.StaffMembers
                .Where(member => member.DeletedAt == null)
                .Select(member => new StaffMemberItem(
                    member.Id.Value,
                    member.FullName.Value,
                    member.Email.Value,
                    member.Role))
                .ToList()
        };
    }
}


