using MassTransit;

using TFTStats.Summoners.Persistence;

namespace TFTStats.Summoners.Features.Summoners.SendSummonersPuuid;

public sealed class GetSummonersIdConsumer(ApplicationDbContext context, IPublishEndpoint publishEndpoint) : IConsumer<GetSummonersIdEvent>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    public Task Consume(ConsumeContext<GetSummonersIdEvent> context)
    {
        var summonerPuuids = _context.Summoners.Select(summoner => summoner.Puuid).ToList();
        _publishEndpoint.Publish(new SendSummonersPuuidEvent(summonerPuuids));
        return Task.CompletedTask;
    }
}
