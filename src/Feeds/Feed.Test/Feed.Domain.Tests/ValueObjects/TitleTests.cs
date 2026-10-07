using SharedKernel.Exceptions;
using SharedKernel.ValuesObjects;

namespace Feeds.Domain.Tests.ValueObjects;

public class TitleTests
{
    [Fact]
    public void Title_WithValidValue_ShouldCreateTitle()
    {
        // Arrange
        var value = "Test title";

        // Act
        var title = new Title(value);

        // Assert
        Assert.Equal(value, title.Value);
    }

    [Fact]
    public void Title_WithEmptyValue_ShouldThrowDomainException()
    {
        // Act
        var action = () => new Title("");

        // Assert
        var exception = Assert.Throws<DomainException>(action);

        Assert.Equal(
            "Title is required.",
            exception.Message);
    }

    [Fact]
    public void Title_WithWhitespaceOnly_ShouldThrowDomainException()
    {
        // Act
        var action = () => new Title("   ");

        // Assert
        Assert.Throws<DomainException>(action);
    }

    [Fact]
    public void Title_WithLeadingAndTrailingSpaces_ShouldTrimValue()
    {
        // Arrange
        var value = "   Test title   ";

        // Act
        var title = new Title(value);

        // Assert
        Assert.Equal(
            "Test title",
            title.Value);
    }

    [Fact]
    public void Title_WithExactly200Characters_ShouldCreateTitle()
    {
        // Arrange
        var value = new string('a', 200);

        // Act
        var title = new Title(value);

        // Assert
        Assert.Equal(200, title.Value.Length);
    }

    [Fact]
    public void Title_WithMoreThan200Characters_ShouldThrowDomainException()
    {
        // Arrange
        var value = new string('a', 201);

        // Act
        var action = () => new Title(value);

        // Assert
        var exception = Assert.Throws<DomainException>(action);

        Assert.Equal(
            "Title cannot exceed 200 characters.",
            exception.Message);
    }
}