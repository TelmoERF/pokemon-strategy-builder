using PokemonStrategyBuilder.Domain.Enums;

namespace PokemonStrategyBuilder.Application.DTOs;

public class OffensiveCoverageDto
{
    public int TeamId { get; set; }
    public string TeamName { get; set; } = string.Empty;
    public int CoverageScore { get; set; }
    public List<PokemonType> AvailableAttackingTypes { get; set; } = [];
    public List<OffensiveCoverageEntryDto> CoveredTypes { get; set; } = [];
    public List<PokemonType> UncoveredTypes { get; set; } = [];
}
