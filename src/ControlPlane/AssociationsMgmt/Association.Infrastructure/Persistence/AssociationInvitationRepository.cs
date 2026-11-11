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



    /// <summary>
    ///        Checks if an invitation exists and pending for the given email and association id
    /// </summary>
    /// <param name="associationId"></param>
    /// <param name="email"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<bool> IsPendingInvitationExistsAsync(
        AssociationID associationId,
        ContactEmail email,
        CancellationToken ct = default)
    {
        return await context.Invitations
            .AnyAsync(
                invitation =>
                    invitation.AssociationID == associationId &&
                    invitation.Email == email &&
                    invitation.Status == InvitationStatus.Pending,
                ct);
    }



    /// <summary>
    ///            Adds a new invitation to the database
    /// </summary>
    /// <param name="invitation"></param>
    /// <param name="ct"></param>
    /// <returns></returns>

    public async Task AddAsync(
        AssociationInvitation invitation,
        CancellationToken ct = default)
    {
        await context.Invitations.AddAsync(invitation, ct);
    }


    /// <summary>
    ///         Gets an invitation by its token hash
    /// </summary>
    /// <param name="tokenHash"></param>
    /// <param name="ct"></param>
    /// <returns></returns>

    public async Task<AssociationInvitation?> GetByTokenHashAsync(
    InvitationTokenHash tokenHash,
    CancellationToken ct = default)
    {
        return await context.Invitations.FirstOrDefaultAsync(
                invitation => invitation.TokenHash == tokenHash,
                ct);
    }

    /// <summary>
    ///           Gets an invitation by its id
    /// </summary>
    /// <param name="invitationId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>

    public async Task<AssociationInvitation?> GetByIdAsync(
        InvitationID invitationId,
        CancellationToken ct = default)
    {

        return await context.Invitations.FirstOrDefaultAsync(
            invitation => invitation.Id == invitationId,
            ct);
    }
    public async Task<int> GetPendingInvitationCountAsync(
    AssociationID associationId,
    CancellationToken ct = default)
    {
        return await context.Invitations
            .CountAsync(
                invitation =>
                    invitation.AssociationID == associationId &&
                    invitation.Status == InvitationStatus.Pending,
                ct);
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await context.SaveChangesAsync(ct);
    }
}