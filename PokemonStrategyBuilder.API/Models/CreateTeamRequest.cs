namespace PokemonStrategyBuilder.API.Models;

public class CreateTeamRequest
{
    public string Name { get; set; } = string.Empty;
    public List<string> PokemonNames { get; set; } = [];
}
