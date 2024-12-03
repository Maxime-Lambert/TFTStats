using Microsoft.Extensions.Options;

namespace TFTStats.Matches.Options;

internal sealed class RiotApiOptionsSetup(IConfiguration configuration) : IConfigureOptions<RiotApiOptions>
{
    private const string ConfigurationSection = "RiotApi";
    private readonly IConfiguration _configuration = configuration;

    public void Configure(RiotApiOptions options)
    {
        if (options is not null)
        {
            _configuration.GetSection(ConfigurationSection).Bind(options);
        }
    }
}
