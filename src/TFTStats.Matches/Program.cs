using HealthChecks.UI.Client;

using MassTransit;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;

using Refit;

using Serilog;

using TFTStats.Matches.Features.Compositions.GetFrequentCompositions;
using TFTStats.Matches.Features.Matches.GetAllMatches;
using TFTStats.Matches.Options;
using TFTStats.Matches.Persistence;
using TFTStats.Matches.RiotApi;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddHealthChecks();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

builder.Services
    .AddRefitClient<IRiotApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["RiotApi:EUW"]!));

builder.Services.ConfigureOptions<RiotApiOptionsSetup>();

builder.Services.AddDbContext<ApplicationDbContext>((dbContextOptionsBuilder) =>
{
    _ = dbContextOptionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("Database")!);
    _ = dbContextOptionsBuilder.EnableDetailedErrors();
    _ = dbContextOptionsBuilder.EnableSensitiveDataLogging();
});

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();

    _ = busConfigurator.AddConsumer<SendSummonersIdConsumer>();

    busConfigurator.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(builder.Configuration["MessageBroker:Host"]!, host =>
        {
            host.Username(builder.Configuration["MessageBroker:Username"]!);
            host.Password(builder.Configuration["MessageBroker:Password"]!);
        });

        configurator.ConfigureEndpoints(context);
    });
});

builder.Services.AddScoped<IGetMatches, GetSummonersPuuidEventPublisher>();
builder.Services.AddScoped<ICalculateFrequentCompositions, SpmfCompositionService>();

var app = builder.Build();

if (!app.Environment.IsProduction())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await db.Database.MigrateAsync().ConfigureAwait(false);
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.MapGet("/requestMatchesInformations", async (IGetMatches matches) => await matches.PublishGetSummonersPuuidEvent().ConfigureAwait(false));

app.MapGet("/mostUsedCompositions", async (ICalculateFrequentCompositions compositions) => await compositions.GetFrequentCompositionsAsync().ConfigureAwait(false));

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

await app.RunAsync().ConfigureAwait(false);
