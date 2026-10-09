using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using Feeds.Application.Contracts;
using Feeds.Domain.Feeds;
using SharedKernel.ValueObjects;

namespace Feeds.Application.Commands.UpdateFeed;

public sealed class UpdateFeedCommandHandler
    : ICommandHandler<UpdateFeedCommand, bool>
{
    private readonly IFeedRepo _feedRepository;

    public UpdateFeedCommandHandler(IFeedRepo feedRepository)
    {
        _feedRepository = feedRepository;
    }

    public async Task<bool> Handle(
        UpdateFeedCommand command,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var feed = await _feedRepository.GetByIdAsync(
            new FeedID(command.FeedId),
            cancellationToken);

        if (feed is null)
        {
            throw new KeyNotFoundException(
                $"Feed {command.FeedId} was not found.");
        }

        feed.Update(
            new Title(command.Title),
            new Description(command.Content));

        await _feedRepository.UpdateFeedAsync(
            feed,
            cancellationToken);

        return true;
    }
}