using Contracts.SummonerEvents;

using MassTransit;

using TFTStats.Summoners.Persistence;

namespace TFTStats.Summoners.Features.Summoners.SendSummonersPuuid;

internal sealed class GetSummonersIdConsumer(ApplicationDbContext context, IPublishEndpoint publishEndpoint, ILogger<GetSummonersIdConsumer> logger) : IConsumer<GetSummonersPuuidEvent>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
    private readonly ILogger<GetSummonersIdConsumer> _logger = logger;

    public Task Consume(ConsumeContext<GetSummonersPuuidEvent> context)
    {
        _logger.LogRabbitMQConsumed(nameof(GetSummonersPuuidEvent));
        var summonerPuuids = _context.Summoners.Select(summoner => summoner.Puuid).ToList();
        _ = _publishEndpoint.Publish(new SendSummonersPuuidEvent(summonerPuuids));
        _logger.LogRabbitMQPublished(nameof(SendSummonersPuuidEvent));
        return Task.CompletedTask;
    }
}
