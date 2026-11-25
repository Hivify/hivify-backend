using Association.Domain.Enums;
using Association.Domain.ValueObjects;
using SharedKernel.ValueObjects;

namespace Domain.Tests;

public sealed class MembershipTests
{
    [Fact]
    public void Create_ShouldInitializeMembershipCorrectly()
    {
        // Arrange
        var userId = new UserID(Guid.NewGuid());
        var email = new AssociationEmail("member@example.com");
        var role = AssociationRoles.AssociationMember;

        // Act
        var membership = Membership.Create(
            userId,
            role,
            email);

        // Assert
        Assert.NotEqual(Guid.Empty, membership.Id.Value);
        Assert.Equal(userId, membership.UserID);
        Assert.Equal(email, membership.MemberEmail);
        Assert.Equal(role, membership.Role);
        Assert.NotEqual(default, membership.JoinedAt);
    }
}