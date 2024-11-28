using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;

using Serilog;
using Refit;

using TFTStats.Summoners.Persistence;
using TFTStats.Summoners.RiotApi;
using TFTStats.Summoners.Options;
using TFTStats.Summoners.Features.Summoners.GetAllSummoners;
using MassTransit;
using TFTStats.Summoners.Features.Summoners.SendSummonersPuuid;

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

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();

    busConfigurator.AddConsumer<GetSummonersIdConsumer>();

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

builder.Services.AddScoped<IGetSummoners, RiotSummonerService>();

builder.Services.AddDbContext<ApplicationDbContext>((dbContextOptionsBuilder) =>
{
    dbContextOptionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("Database")!);
});

var app = builder.Build();

if (!app.Environment.IsProduction())
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await db.Database.MigrateAsync().ConfigureAwait(false);
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.MapGet("/master", async (IGetSummoners service) =>
{
    await service.AddMasterSummoners().ConfigureAwait(false);
});

app.MapGet("/grandMaster", async (IGetSummoners service) =>
{
    await service.AddGrandMasterSummoners().ConfigureAwait(false);
});

app.MapGet("/challengers", async (IGetSummoners service) =>
{
    await service.AddChallengerSummoners().ConfigureAwait(false);
});

app.MapGet("/rabbit", async (IPublishEndpoint publishEndpoint) =>
{
    await publishEndpoint.Publish(new GetSummonersIdEvent()).ConfigureAwait(false);
});

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

await app.RunAsync().ConfigureAwait(false);
