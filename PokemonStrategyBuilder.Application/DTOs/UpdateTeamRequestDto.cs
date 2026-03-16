namespace PokemonStrategyBuilder.Application.DTOs;

public class UpdateTeamRequestDto
{
    public string Name { get; set; } = string.Empty;
    public List<string> PokemonNames { get; set; } = [];
}
