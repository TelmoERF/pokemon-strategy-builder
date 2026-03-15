using PokemonStrategyBuilder.Domain.Enums;

namespace PokemonStrategyBuilder.API.Models;

public class AnalyzeTeamRequest
{
    public List<PokemonTypeInput> Pokemon { get; set; } = [];
}

public class PokemonTypeInput
{
    public PokemonType PrimaryType { get; set; }
    public PokemonType? SecondaryType { get; set; }
}
