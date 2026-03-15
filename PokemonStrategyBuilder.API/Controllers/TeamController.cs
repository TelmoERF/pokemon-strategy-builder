using Microsoft.AspNetCore.Mvc;
using PokemonStrategyBuilder.API.Models;
using PokemonStrategyBuilder.Application.Interfaces;
using PokemonStrategyBuilder.Domain.Entities;

namespace PokemonStrategyBuilder.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamController : ControllerBase
{
    private readonly ITeamWeaknessAnalyzerService _analyzer;

    public TeamController(ITeamWeaknessAnalyzerService analyzer)
    {
        _analyzer = analyzer;
    }

    [HttpPost("analyze")]
    public IActionResult Analyze([FromBody] AnalyzeTeamRequest request)
    {
        if (request.Pokemon is null || request.Pokemon.Count == 0)
        {
            return BadRequest("At least one Pokémon must be provided.");
        }

        var team = request.Pokemon.Select(p => new Pokemon(
            id: 0,
            name: "Unknown",
            primaryType: p.PrimaryType,
            secondaryType: p.SecondaryType,
            hp: 0,
            attack: 0,
            defense: 0,
            specialAttack: 0,
            specialDefense: 0,
            speed: 0)).ToList();

        var result = _analyzer.Analyze(team);

        return Ok(result);
    }
}
