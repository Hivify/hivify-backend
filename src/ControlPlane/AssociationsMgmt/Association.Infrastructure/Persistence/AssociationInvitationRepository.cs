using Association.Application.Contracts;
using Association.Domain.Entities;
using Association.Domain.Enums;
using Association.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using SharedKernel.ValueObjects;

namespace Association.Infrastructure.Persistence;

public sealed class AssociationInvitationRepository(
    AssociationDbContext context)
    : IAssociationInvitationRepository
{
    public async Task<bool> IsPendingInvitationExistsAsync(
        AssociationID associationId,
        string email,
        CancellationToken ct = default)
    {
        return await context.Invitations
            .AnyAsync(
                invitation =>
                    invitation.AssociationID == associationId &&
                    invitation.Email.Value == email &&
                    invitation.Status == InvitationStatus.Pending,
                ct);
    }

    public async Task AddAsync(
        AssociationInvitation invitation,
        CancellationToken ct = default)
    {
        await context.Invitations.AddAsync(invitation, ct);
    }

    public async Task<AssociationInvitation?> GetByTokenHashAsync(
    InvitationTokenHash tokenHash,
    CancellationToken ct = default)
    {
        return await context.Invitations.FirstOrDefaultAsync(
                invitation => invitation.TokenHash == tokenHash,
                ct);
    }

    public async Task<AssociationInvitation?> GetByIdAsync(
        InvitationID invitationId,
        CancellationToken ct = default)
    {

        return await context.Invitations.FirstOrDefaultAsync(
            invitation => invitation.Id == invitationId,
            ct);
    }


    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await context.SaveChangesAsync(ct);
    }
}