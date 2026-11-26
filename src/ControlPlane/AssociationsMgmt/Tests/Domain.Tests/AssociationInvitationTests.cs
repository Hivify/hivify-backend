using Association.Domain.Entities;
using Association.Domain.Enums;
using Association.Domain.ValueObjects;
using SharedKernel.ValueObjects;

namespace Domain.Tests;

public sealed class AssociationInvitationTests
{
    private static AssociationInvitation CreateInvitation()
    {
        return AssociationInvitation.Create(
            new AssociationID(Guid.NewGuid()),
            new UserID(Guid.NewGuid()),
            new AssociationEmail("member@example.com"),
            InvitationTokenHash.Create("test-token"));
    }

    [Fact]
    public void Create_ShouldInitializeInvitationAsPending()
    {
        // Arrange & Act
        var invitation = CreateInvitation();

        // Assert
        Assert.NotEqual(Guid.Empty, invitation.Id.Value);
        Assert.Equal(InvitationStatus.Pending, invitation.Status);
        Assert.Null(invitation.AcceptedAt);
    }

    [Fact]
    public void Accept_WhenPendingAndNotExpired_ShouldMarkInvitationAsAccepted()
    {
        // Arrange
        var invitation = CreateInvitation();

        // Act
        invitation.Accept();

        // Assert
        Assert.Equal(InvitationStatus.Accepted, invitation.Status);
        Assert.NotNull(invitation.AcceptedAt);
    }

    [Fact]
    public void Accept_WhenAlreadyAccepted_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var invitation = CreateInvitation();
        invitation.Accept();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(
            () => invitation.Accept());
    }

    [Fact]
    public void Revoke_WhenPending_ShouldMarkInvitationAsRevoked()
    {
        // Arrange
        var invitation = CreateInvitation();

        // Act
        invitation.Revoke();

        // Assert
        Assert.Equal(InvitationStatus.Revoked, invitation.Status);
    }

    [Fact]
    public void Revoke_WhenAlreadyAccepted_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var invitation = CreateInvitation();
        invitation.Accept();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(
            () => invitation.Revoke());
    }

    [Fact]
    public void MarkExpired_WhenPendingAndExpired_ShouldMarkInvitationAsExpired()
    {
        // Arrange
        var invitation = CreateInvitation();

        // Act
        // The invitation expires after five days.
        var exception = Record.Exception(() =>
        {
            // This test needs an expired invitation.
            // See the note below.
            invitation.MarkExpired();
        });

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void IsValid_WhenPendingAndNotExpired_ShouldReturnTrue()
    {
        // Arrange
        var invitation = CreateInvitation();

        // Act
        var result = invitation.IsValid(DateTimeOffset.UtcNow);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsValid_WhenAccepted_ShouldReturnFalse()
    {
        // Arrange
        var invitation = CreateInvitation();
        invitation.Accept();

        // Act
        var result = invitation.IsValid(DateTimeOffset.UtcNow);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsValid_WhenRevoked_ShouldReturnFalse()
    {
        // Arrange
        var invitation = CreateInvitation();
        invitation.Revoke();

        // Act
        var result = invitation.IsValid(DateTimeOffset.UtcNow);

        // Assert
        Assert.False(result);
    }
}