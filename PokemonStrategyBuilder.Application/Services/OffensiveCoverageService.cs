using PokemonStrategyBuilder.Application.DTOs;
using PokemonStrategyBuilder.Application.Interfaces;
using PokemonStrategyBuilder.Domain.Entities;
using PokemonStrategyBuilder.Domain.Enums;
using PokemonStrategyBuilder.Domain.Interfaces;

namespace PokemonStrategyBuilder.Application.Services;

public class OffensiveCoverageService : IOffensiveCoverageService
{
    private readonly ITypeEffectivenessService _typeEffectivenessService;

    public OffensiveCoverageService(ITypeEffectivenessService typeEffectivenessService)
    {
        _typeEffectivenessService = typeEffectivenessService;
    }

    public OffensiveCoverageDto Analyze(int teamId, string teamName, IReadOnlyCollection<Pokemon> pokemon)
    {
        var availableAttackingTypes = pokemon
            .SelectMany(p => new[] { p.PrimaryType, p.SecondaryType })
            .Where(t => t.HasValue)
            .Select(t => t!.Value)
            .Distinct()
            .OrderBy(t => t)
            .ToList();

        var coveredTypes = new List<OffensiveCoverageEntryDto>();
        var uncoveredTypes = new List<PokemonType>();

        foreach (PokemonType defendingType in Enum.GetValues(typeof(PokemonType)))
        {
            var coveringAttackTypes = availableAttackingTypes
                .Where(attackingType => _typeEffectivenessService.GetEffectiveness(attackingType, defendingType) > 1.0)
                .ToList();

            if (coveringAttackTypes.Count > 0)
            {
                coveredTypes.Add(new OffensiveCoverageEntryDto
                {
                    DefendingType = defendingType,
                    CoveredByAttackingTypes = coveringAttackTypes
                });
            }
            else
            {
                uncoveredTypes.Add(defendingType);
            }
        }

        var coverageScore = (int)Math.Round((double)coveredTypes.Count / Enum.GetValues<PokemonType>().Length * 100);

        return new OffensiveCoverageDto
        {
            TeamId = teamId,
            TeamName = teamName,
            CoverageScore = coverageScore,
            AvailableAttackingTypes = availableAttackingTypes,
            CoveredTypes = coveredTypes.OrderBy(x => x.DefendingType).ToList(),
            UncoveredTypes = uncoveredTypes.OrderBy(x => x).ToList()
        };
    }
}
