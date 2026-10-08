using SharedKernel.Exceptions;
using SharedKernel.ValueObjects;

namespace Feeds.Domain.Tests.ValueObjects;

public class DescriptionTests
{
    [Fact]
    public void Description_WithValidValue_ShouldCreateDescription()
    {
        // Arrange
        var value = "Test content";

        // Act
        var description = new Description(value);

        // Assert
        Assert.Equal(value, description.Value);
    }

    [Fact]
    public void Description_WithEmptyValue_ShouldThrowDomainException()
    {
        // Act
        var action = () => new Description("");

        // Assert
        var exception = Assert.Throws<DomainException>(action);

        Assert.Equal(
            "Content is required.",
            exception.Message);
    }

    [Fact]
    public void Description_WithWhitespaceOnly_ShouldThrowDomainException()
    {
        // Act
        var action = () => new Description("   ");

        // Assert
        Assert.Throws<DomainException>(action);
    }

    [Fact]
    public void Description_WithLeadingAndTrailingSpaces_ShouldTrimValue()
    {
        // Arrange
        var value = "   Test content   ";

        // Act
        var description = new Description(value);

        // Assert
        Assert.Equal(
            "Test content",
            description.Value);
    }

    [Fact]
    public void Description_WithExactly1000Characters_ShouldCreateDescription()
    {
        // Arrange
        var value = new string('a', 1000);

        // Act
        var description = new Description(value);

        // Assert
        Assert.Equal(1000, description.Value.Length);
    }

    [Fact]
    public void Description_WithMoreThan1000Characters_ShouldThrowDomainException()
    {
        // Arrange
        var value = new string('a', 1001);

        // Act
        var action = () => new Description(value);

        // Assert
        var exception = Assert.Throws<DomainException>(action);

        Assert.Equal(
            "Content cannot exceed 1000 characters.",
            exception.Message);
    }
}