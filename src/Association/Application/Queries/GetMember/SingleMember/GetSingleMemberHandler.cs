using Association.Application.Contracts;
using Association.Application.DTOs;
using Association.Domain.Associations;
using BuildingBlocks.ApplicationPorts.Messaging;

namespace Association.Application.Queries.GetMember.SingleMember;

public sealed class GetSingleMemberQueryHandler
    : IQueryHandler<GetSingleMemberQuery, StaffMemberItem>
{
    private readonly IAssociationRepo _associationRepository;

    public GetSingleMemberQueryHandler(
        IAssociationRepo associationRepository)
    {
        _associationRepository = associationRepository;
    }

    public async Task<StaffMemberItem> Handle(
        GetSingleMemberQuery query,
        CancellationToken cancellationToken)
    {
        var association =
            await _associationRepository.GetByIdAsync(
                new AssociationID(query.AssociationId),
                cancellationToken);

        if (association is null)
            throw new InvalidOperationException(
                "Association was not found.");

        var member = association.StaffMembers
            .FirstOrDefault(member =>
                member.Id.Value == query.MemberId &&
                member.DeletedAt == null);

        if (member is null)
            throw new InvalidOperationException(
                "Member was not found.");

        return new StaffMemberItem(
            member.Id.Value,
            member.FullName.Value,
            member.Email.Value,
            member.Role);
    }
}