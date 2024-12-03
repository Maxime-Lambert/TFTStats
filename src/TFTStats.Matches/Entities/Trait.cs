namespace TFTStats.Matches.Entities;

internal sealed class Trait
{
    public string? Name { get; set; }
    public int? NumberOfUnits { get; set; }
    public int? CurrentTier { get; set; }
    public int? TotalTier { get; set; }
    public string? ParticipantId { get; set; }
    public string? MatchId { get; set; }
    public Guid Id { get; set; }

    public Trait()
    { }

    public Trait(string? name, int? numberOfUnits, int? currentTier, int? totalTier, string participantId, Guid id, string? matchId)
    {
        Name = name;
        NumberOfUnits = numberOfUnits;
        CurrentTier = currentTier;
        TotalTier = totalTier;
        ParticipantId = participantId;
        Id = id;
        MatchId = matchId;
    }
}