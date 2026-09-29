using Association.Application.Contracts;
using Association.Application.DTOs;
using Association.Domain.Associations;
using BuildingBlocks.ApplicationPorts.Messaging;

namespace Association.Application.Queries.GetMember.AllMembers;

public sealed class GetMembersQueryHandler
    : IQueryHandler<GetMembersQuery, IReadOnlyList<StaffMemberItem>>
{
    private readonly IAssociationRepo _associationRepository;

    public GetMembersQueryHandler(
        IAssociationRepo associationRepository)
    {
        _associationRepository = associationRepository;
    }

    public async Task<IReadOnlyList<StaffMemberItem>> Handle(
        GetMembersQuery query,
        CancellationToken cancellationToken)
    {
        var association =
            await _associationRepository.GetByIdAsync(
                new AssociationID(query.AssociationId),
                cancellationToken);

        if (association is null)
            throw new InvalidOperationException(
                "Association was not found.");

        return association.StaffMembers
            .Where(member => member.DeletedAt == null)
            .Select(member => new StaffMemberItem(
                member.Id.Value,
                member.FullName.Value,
                member.Email.Value,
                member.Role))
            .ToList();
    }
}