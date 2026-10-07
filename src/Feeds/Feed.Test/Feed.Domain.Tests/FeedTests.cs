using Feeds.Domain.Feeds;
using SharedKernel.ValuesObjects;

namespace Feeds.Domain.Tests;

public class FeedTests
{
    [Fact]
    public void CreateFeed_WithValidData_ShouldCreateFeed()
    {
        // Arrange
        var authorId = new UserID(Guid.NewGuid());
        var title = new Title("Test title");
        var content = new Description("Test content");

        // Act
        var feed = Feed.CreateFeed(
            authorId,
            title,
            content);

        // Assert
        Assert.NotNull(feed);
    }

    [Fact]
    public void CreateFeed_WithValidData_ShouldSetProperties()
    {
        // Arrange
        var authorId = new UserID(Guid.NewGuid());
        var title = new Title("Test title");
        var content = new Description("Test content");

        // Act
        var feed = Feed.CreateFeed(
            authorId,
            title,
            content);

        // Assert
        Assert.NotEqual(Guid.Empty, feed.Id.Value);
        Assert.Equal(authorId, feed.AuthorId);
        Assert.Equal(title, feed.Title);
        Assert.Equal(content, feed.Content);
        Assert.Null(feed.DeletedAt);
    }
}