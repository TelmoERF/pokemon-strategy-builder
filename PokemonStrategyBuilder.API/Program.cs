using PokemonStrategyBuilder.Application.Interfaces;
using PokemonStrategyBuilder.Application.Services;
using PokemonStrategyBuilder.Domain.Interfaces;
using PokemonStrategyBuilder.Domain.Services;
using System.Text.Json.Serialization;

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


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
