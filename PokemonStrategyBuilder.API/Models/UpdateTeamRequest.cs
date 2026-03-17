namespace PokemonStrategyBuilder.API.Models;

public class UpdateTeamRequest
{
    public string Name { get; set; } = string.Empty;
    public List<TeamPokemonSlotRequest> Pokemon { get; set; } = [];
}
