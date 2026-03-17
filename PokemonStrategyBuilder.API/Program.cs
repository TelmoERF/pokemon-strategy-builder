using PokemonStrategyBuilder.Application.Interfaces;
using PokemonStrategyBuilder.Application.Services;
using PokemonStrategyBuilder.Domain.Interfaces;
using PokemonStrategyBuilder.Domain.Services;
using PokemonStrategyBuilder.Infrastructure.Clients;
using PokemonStrategyBuilder.Infrastructure.Services;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using PokemonStrategyBuilder.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITypeEffectivenessService, TypeEffectivenessService>();
builder.Services.AddScoped<ITeamWeaknessAnalyzerService, TeamWeaknessAnalyzerService>();
builder.Services.AddHttpClient<PokeApiClient>(client =>
{
    client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
});
builder.Services.AddScoped<IPokemonDataService, PokemonDataService>();
builder.Services.AddMemoryCache();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=pokemon-strategy-builder.db"));

builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<ITeamRatingService, TeamRatingService>();
builder.Services.AddScoped<IOffensiveCoverageService, OffensiveCoverageService>();
builder.Services.AddScoped<IMoveRepository, MoveRepository>();
builder.Services.AddScoped<IMoveDataService, MoveDataService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
