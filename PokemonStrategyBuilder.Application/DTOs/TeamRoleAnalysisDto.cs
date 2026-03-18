namespace PokemonStrategyBuilder.Application.DTOs;

public class TeamRoleAnalysisDto
{
    public int TeamId { get; set; }
    public string TeamName { get; set; } = string.Empty;
    public List<TeamPokemonRoleDto> PokemonRoles { get; set; } = [];
}