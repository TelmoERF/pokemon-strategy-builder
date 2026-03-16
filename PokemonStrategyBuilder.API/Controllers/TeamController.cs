using Microsoft.AspNetCore.Mvc;
using PokemonStrategyBuilder.API.Models;
using PokemonStrategyBuilder.Application.Interfaces;

namespace PokemonStrategyBuilder.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamController : ControllerBase
{
    private readonly ITeamWeaknessAnalyzerService _analyzer;
    private readonly IPokemonDataService _pokemonDataService;

    public TeamController(
        ITeamWeaknessAnalyzerService analyzer,
        IPokemonDataService pokemonDataService)
    {
        _analyzer = analyzer;
        _pokemonDataService = pokemonDataService;
    }

    [HttpPost("analyze")]
    public async Task<IActionResult> Analyze(
        [FromBody] AnalyzeTeamByNameRequest request,
        CancellationToken cancellationToken)
    {
        if (request.PokemonNames is null || request.PokemonNames.Count == 0)
        {
            return BadRequest("At least one Pokémon name must be provided.");
        }

        if (request.PokemonNames.Count > 6)
        {
            return BadRequest("A team cannot contain more than 6 Pokémon.");
        }

        var team = new List<PokemonStrategyBuilder.Domain.Entities.Pokemon>();
        var notFound = new List<string>();

        foreach (var name in request.PokemonNames)
        {
            var pokemon = await _pokemonDataService.GetPokemonByNameAsync(name, cancellationToken);

            if (pokemon is null)
            {
                notFound.Add(name);
                continue;
            }

            team.Add(pokemon);
        }

        if (notFound.Count > 0)
        {
            return NotFound(new
            {
                Message = "Some Pokémon could not be found.",
                NotFoundPokemon = notFound
            });
        }

        var result = _analyzer.Analyze(team);

        return Ok(new
        {
            Team = team.Select(p => new
            {
                p.Id,
                p.Name,
                p.PrimaryType,
                p.SecondaryType
            }),
            Weaknesses = result
        });
    }
}
