namespace PokemonStrategyBuilder.Application.DTOs;

public class CreateTeamRequestDto
{
    public string Name { get; set; } = string.Empty;
    public List<TeamPokemonSlotRequestDto> Pokemon { get; set; } = [];
}
