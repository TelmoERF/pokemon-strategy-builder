namespace PokemonStrategyBuilder.Application.DTOs;

public class CreateTeamRequestDto
{
    public string Name { get; set; } = string.Empty;
    public List<string> PokemonNames { get; set; } = [];
}
