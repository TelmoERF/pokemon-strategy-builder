namespace PokemonStrategyBuilder.Application.DTOs;

public class TeamPokemonRoleDto
{
    public string PokemonName { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = [];
}
