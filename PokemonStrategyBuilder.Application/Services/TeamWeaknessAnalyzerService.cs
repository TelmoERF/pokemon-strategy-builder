using PokemonStrategyBuilder.Application.DTOs;
using PokemonStrategyBuilder.Application.Interfaces;
using PokemonStrategyBuilder.Domain.Entities;
using PokemonStrategyBuilder.Domain.Enums;
using PokemonStrategyBuilder.Domain.Interfaces;

namespace PokemonStrategyBuilder.Application.Services;

public class TeamWeaknessAnalyzerService : ITeamWeaknessAnalyzerService
{
    private readonly ITypeEffectivenessService _typeEffectivenessService;

    public TeamWeaknessAnalyzerService(ITypeEffectivenessService typeEffectivenessService)
    {
        _typeEffectivenessService = typeEffectivenessService;
    }

    public IReadOnlyCollection<TypeWeaknessDto> Analyze(IEnumerable<Pokemon> team)
    {
        var teamList = team.ToList();

        if (teamList.Count == 0)
        {
            return [];
        }

        var results = new List<TypeWeaknessDto>();

        foreach (PokemonType attackingType in Enum.GetValues(typeof(PokemonType)))
        {
            int weakCount = 0;
            int resistantCount = 0;
            int immuneCount = 0;
            double totalMultiplier = 0;

            foreach (var pokemon in teamList)
            {
                var multiplier = _typeEffectivenessService.GetEffectiveness(
                    attackingType,
                    pokemon.PrimaryType,
                    pokemon.SecondaryType);

                totalMultiplier += multiplier;

                if (multiplier == 0)
                {
                    immuneCount++;
                }
                else if (multiplier > 1)
                {
                    weakCount++;
                }
                else if (multiplier < 1)
                {
                    resistantCount++;
                }
            }

            results.Add(new TypeWeaknessDto
            {
                AttackingType = attackingType,
                WeakPokemonCount = weakCount,
                ResistantPokemonCount = resistantCount,
                ImmunePokemonCount = immuneCount,
                TotalMultiplier = totalMultiplier
            });
        }

        return results
            .OrderByDescending(x => x.WeakPokemonCount)
            .ThenByDescending(x => x.TotalMultiplier)
            .ToList();
    }
}
