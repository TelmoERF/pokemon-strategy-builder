using PokemonStrategyBuilder.Application.DTOs;
using PokemonStrategyBuilder.Application.Interfaces;
using PokemonStrategyBuilder.Domain.Entities;

namespace PokemonStrategyBuilder.Application.Services;

public class TeamRatingService : ITeamRatingService
{
    public TeamRatingDto Rate(
        int teamId,
        string teamName,
        IReadOnlyCollection<Pokemon> pokemon,
        IReadOnlyCollection<TypeWeaknessDto> weaknesses)
    {
        var weaknessScore = CalculateWeaknessScore(weaknesses);
        var resistanceScore = CalculateResistanceScore(weaknesses);
        var completenessScore = CalculateCompletenessScore(pokemon.Count);

        var overallScore = weaknessScore + resistanceScore + completenessScore;
        overallScore = Math.Clamp(overallScore, 0, 100);

        var summary = BuildSummary(pokemon.Count, weaknesses);

        return new TeamRatingDto
        {
            TeamId = teamId,
            TeamName = teamName,
            OverallScore = overallScore,
            WeaknessScore = weaknessScore,
            ResistanceScore = resistanceScore,
            CompletenessScore = completenessScore,
            Summary = summary
        };
    }

    private static int CalculateWeaknessScore(IReadOnlyCollection<TypeWeaknessDto> weaknesses)
    {
        var severeWeaknesses = weaknesses.Count(w => w.WeakPokemonCount >= 2);
        var extremeWeaknesses = weaknesses.Count(w => w.TotalMultiplier >= 5);

        var score = 40;
        score -= severeWeaknesses * 6;
        score -= extremeWeaknesses * 4;

        return Math.Clamp(score, 0, 40);
    }

    private static int CalculateResistanceScore(IReadOnlyCollection<TypeWeaknessDto> weaknesses)
    {
        var strongDefensiveTypes = weaknesses.Count(w =>
            w.ResistantPokemonCount + w.ImmunePokemonCount >= 2);

        var immunities = weaknesses.Sum(w => w.ImmunePokemonCount);

        var score = 20;
        score += Math.Min(strongDefensiveTypes * 2, 10);
        score += Math.Min(immunities, 10);

        return Math.Clamp(score, 0, 40);
    }

    private static int CalculateCompletenessScore(int pokemonCount)
    {
        return pokemonCount switch
        {
            6 => 20,
            5 => 16,
            4 => 12,
            3 => 8,
            2 => 4,
            1 => 2,
            _ => 0
        };
    }

    private static List<string> BuildSummary(int pokemonCount, IReadOnlyCollection<TypeWeaknessDto> weaknesses)
    {
        var summary = new List<string>();

        if (pokemonCount < 6)
        {
            summary.Add($"Team is incomplete with only {pokemonCount} Pokémon.");
        }
        else
        {
            summary.Add("Team has full size.");
        }

        var biggestThreats = weaknesses
            .Where(w =>
                w.WeakPokemonCount >= 2 ||
                w.TotalMultiplier >= 5)
            .OrderByDescending(w => w.WeakPokemonCount)
            .ThenByDescending(w => w.TotalMultiplier)
            .Take(3)
            .Select(w => w.AttackingType.ToString())
            .ToList();

        if (biggestThreats.Count > 0)
        {
            summary.Add($"Main defensive threats: {string.Join(", ", biggestThreats)}.");
        }
        else
        {
            summary.Add("No major shared defensive threats detected.");
        }

        var defensiveStrengths = weaknesses
            .Where(w =>
                !biggestThreats.Contains(w.AttackingType.ToString()) &&
                (w.ResistantPokemonCount + w.ImmunePokemonCount >= 2) &&
                w.WeakPokemonCount == 0)
            .OrderByDescending(w => w.ResistantPokemonCount + w.ImmunePokemonCount)
            .ThenBy(w => w.TotalMultiplier)
            .Take(3)
            .Select(w => w.AttackingType.ToString())
            .ToList();

        if (defensiveStrengths.Count > 0)
        {
            summary.Add($"Best defensive coverage against: {string.Join(", ", defensiveStrengths)}.");
        }

        return summary;
    }
}
