namespace TFTStats.Summoners.Features.Summoners.SendSummonersPuuid;

public sealed record SendSummonersPuuidEvent(IEnumerable<string> SummonersPuuids) { }