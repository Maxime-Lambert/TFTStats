using Microsoft.EntityFrameworkCore;

using TFTStats.Summoners.Entities;

namespace TFTStats.Summoners.Persistence;

internal sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Summoner> Summoners { get; set; }
}