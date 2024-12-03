namespace TFTStats.Matches.Entities;

internal sealed class Unit
{
    public string? Name { get; set; }
    public int? Tier { get; set; }
    public IEnumerable<string> ItemNames { get; set; }
    public string? ParticipantId { get; set; }
    public Guid Id { get; set; }
    public string? MatchId { get; set; }

    public Unit()
    {
        ItemNames = [];
    }

    public Unit(string? name, int? tier, IEnumerable<string> itemNames, string participantId, Guid id, string? matchId)
    {
        Name = name;
        Tier = tier;
        ItemNames = itemNames;
        ParticipantId = participantId;
        Id = id;
        MatchId = matchId;
    }
}