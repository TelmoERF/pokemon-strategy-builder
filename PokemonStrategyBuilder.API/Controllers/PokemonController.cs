using Microsoft.AspNetCore.Mvc;
using PokemonStrategyBuilder.Application.Interfaces;

namespace PokemonStrategyBuilder.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PokemonController : ControllerBase
{
    private readonly IPokemonDataService _pokemonDataService;

    public PokemonController(IPokemonDataService pokemonDataService)
    {
        _pokemonDataService = pokemonDataService;
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetByName(string name, CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonDataService.GetPokemonByNameAsync(name, cancellationToken);

        if (pokemon is null)
        {
            return NotFound();
        }

        return Ok(pokemon);
    }
}
