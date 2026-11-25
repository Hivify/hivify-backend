



using Association.Domain.Entities;
using Association.Domain.Enums;
using Association.Domain.Events;
using Association.Domain.ValueObjects;
using SharedKernel.ValueObjects;

namespace Domain.Tests;

public sealed class AssociationEntityTests
{
    private static AssociationEntity CreateAssociation()
    {
        return AssociationEntity.Create(
            new AssociationName("Hivify"),
            new UserID(Guid.NewGuid()),
            new AssociationEmail("owner@example.com"),
            new AssociationTierID(Guid.NewGuid()));
    }

    [Fact]
    public void Create_ShouldInitializeAssociationCorrectly()
    {
        // Arrange
        var name = new AssociationName("Hivify");
        var ownerId = new UserID(Guid.NewGuid());
        var email = new AssociationEmail("owner@example.com");
        var tierId = new AssociationTierID(Guid.NewGuid());

        // Act
        var association = AssociationEntity.Create(
            name,
            ownerId,
            email,
            tierId);

        // Assert
        Assert.NotEqual(Guid.Empty, association.Id.Value);
        Assert.Equal(name, association.Name);
        Assert.Equal(tierId, association.TierId);
        Assert.Equal(email, association.ContactEmail);
        Assert.Equal(AssociationStatus.Pending, association.Status);
        Assert.NotEqual(default, association.CreatedDate);
    }

    [Fact]
    public void Create_ShouldAddOwnerAsAssociationAdmin()
    {
        // Arrange
        var ownerId = new UserID(Guid.NewGuid());
        var email = new AssociationEmail("owner@example.com");

        // Act
        var association = AssociationEntity.Create(
            new AssociationName("Hivify"),
            ownerId,
            email,
            new AssociationTierID(Guid.NewGuid()));

        // Assert
        var owner = Assert.Single(association.Members);

        Assert.Equal(ownerId, owner.UserID);
        Assert.Equal(email, owner.MemberEmail);
        Assert.Equal(AssociationRoles.AssociationAdmin, owner.Role);
    }

    [Fact]
    public void Create_ShouldRaiseAssociationCreatedDomainEvent()
    {
        // Arrange
        var ownerId = new UserID(Guid.NewGuid());

        // Act
        var association = AssociationEntity.Create(
            new AssociationName("Hivify"),
            ownerId,
            new AssociationEmail("owner@example.com"),
            new AssociationTierID(Guid.NewGuid()));

        // Assert
        Assert.Contains(
            association.DomainEvents,
            domainEvent => domainEvent is AssociationCreatedDomainEvent);
    }

    [Fact]
    public void AddMember_ShouldAddMemberToAssociation()
    {
        // Arrange
        var association = CreateAssociation();
        var userId = new UserID(Guid.NewGuid());
        var email = new AssociationEmail("member@example.com");

        // Act
        association.AddMember(
            userId,
            AssociationRoles.AssociationMember,
            email);

        // Assert
        Assert.Equal(2, association.Members.Count);

        Assert.Contains(
            association.Members,
            member => member.UserID == userId);
    }

    [Fact]
    public void AddMember_WhenUserAlreadyExists_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var association = CreateAssociation();
        var existingMember = Assert.Single(association.Members);

        // Act
        var exception = Assert.Throws<InvalidOperationException>(
            () => association.AddMember(
                existingMember.UserID,
                AssociationRoles.AssociationMember,
                existingMember.MemberEmail));

        // Assert
        Assert.Equal(
            "User is already a member of this association.",
            exception.Message);

        Assert.Single(association.Members);
    }
}
