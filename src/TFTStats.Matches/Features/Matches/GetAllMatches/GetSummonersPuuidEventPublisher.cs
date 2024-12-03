using Contracts.SummonerEvents;

using MassTransit;

namespace TFTStats.Matches.Features.Matches.GetAllMatches;

internal sealed class GetSummonersPuuidEventPublisher(IPublishEndpoint publishEndpoint, ILogger<GetSummonersPuuidEventPublisher> logger) : IGetMatches
{
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
    private readonly ILogger<GetSummonersPuuidEventPublisher> _logger = logger;

    public async Task PublishGetSummonersPuuidEvent()
    {
        await _publishEndpoint.Publish(new GetSummonersPuuidEvent()).ConfigureAwait(false);
        _logger.LogRabbitMQPublished(nameof(GetSummonersPuuidEvent));
    }
}
