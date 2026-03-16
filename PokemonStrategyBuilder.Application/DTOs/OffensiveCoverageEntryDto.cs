using PokemonStrategyBuilder.Domain.Enums;

namespace PokemonStrategyBuilder.Application.DTOs;

public class OffensiveCoverageEntryDto
{
    public PokemonType DefendingType { get; set; }
    public List<PokemonType> CoveredByAttackingTypes { get; set; } = [];
}
