using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TFTStats.Matches.Entities;

namespace TFTStats.Matches.Persistence;

internal sealed class MatchesConfiguration : IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        _ = builder.HasKey(x => x.Id);

        _ = builder.OwnsMany(match => match.Participants, participantsBuilder =>
        {
            _ = participantsBuilder.HasKey(participant => new { participant.Puuid, participant.MatchId });
            _ = participantsBuilder.OwnsMany(participant => participant.Units, unitBuilder =>
            {
                _ = unitBuilder.HasKey(unit => unit.Id);
                _ = unitBuilder.WithOwner().HasForeignKey(unit => new { unit.ParticipantId, unit.MatchId });
            });
            _ = participantsBuilder.OwnsMany(participant => participant.Traits, traitBuilder =>
            {
                _ = traitBuilder.HasKey(trait => trait.Id);
                _ = traitBuilder.WithOwner().HasForeignKey(trait => new { trait.ParticipantId, trait.MatchId });
            });

            _ = participantsBuilder.WithOwner().HasForeignKey(participant => participant.MatchId);
        });
    }
}