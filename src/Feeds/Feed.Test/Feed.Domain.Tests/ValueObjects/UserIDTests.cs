using SharedKernel.Exceptions;
using SharedKernel.ValuesObjects;

namespace Feeds.Domain.Tests.ValueObjects;

public class UserIDTests
{
    [Fact]
    public void UserID_WithValidGuid_ShouldCreateUserID()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var userId = new UserID(guid);

        // Assert
        Assert.Equal(guid, userId.Value);
    }

    [Fact]
    public void UserID_WithEmptyGuid_ShouldThrowDomainException()
    {
        // Act
        Action action = () =>
        {
            _ = new UserID(Guid.Empty);
        };

        // Assert
        var exception = Assert.Throws<DomainException>(action);

        Assert.Equal(
            "Tenant ID cannot be empty.",
            exception.Message);
    }
}