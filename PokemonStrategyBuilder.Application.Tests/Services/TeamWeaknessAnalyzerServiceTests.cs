using PokemonStrategyBuilder.Application.Services;
using PokemonStrategyBuilder.Domain.Entities;
using PokemonStrategyBuilder.Domain.Enums;
using PokemonStrategyBuilder.Domain.Services;

namespace PokemonStrategyBuilder.Application.Tests.Services;

public class TeamWeaknessAnalyzerServiceTests
{
    private readonly TeamWeaknessAnalyzerService _service;

    public TeamWeaknessAnalyzerServiceTests()
    {
        var typeEffectivenessService = new TypeEffectivenessService();
        _service = new TeamWeaknessAnalyzerService(typeEffectivenessService);
    }

    [Fact]
    public void Analyze_ShouldReturnEmpty_WhenTeamIsEmpty()
    {
        var result = _service.Analyze([]);

        Assert.Empty(result);
    }

    [Fact]
    public void Analyze_ShouldIdentifyRockAsAThreat_ForCharizard()
    {
        var team = new List<Pokemon>
        {
            new(
                id: 6,
                name: "Charizard",
                primaryType: PokemonType.Fire,
                secondaryType: PokemonType.Flying,
                hp: 78,
                attack: 84,
                defense: 78,
                specialAttack: 109,
                specialDefense: 85,
                speed: 100)
        };

        var result = _service.Analyze(team);

        var rockAnalysis = result.First(x => x.AttackingType == PokemonType.Rock);

        Assert.Equal(1, rockAnalysis.WeakPokemonCount);
        Assert.Equal(4.0, rockAnalysis.TotalMultiplier);
    }

    [Fact]
    public void Analyze_ShouldIdentifyGroundImmunity_ForCharizard()
    {
        var team = new List<Pokemon>
        {
            new(
                id: 6,
                name: "Charizard",
                primaryType: PokemonType.Fire,
                secondaryType: PokemonType.Flying,
                hp: 78,
                attack: 84,
                defense: 78,
                specialAttack: 109,
                specialDefense: 85,
                speed: 100)
        };

        var result = _service.Analyze(team);

        var groundAnalysis = result.First(x => x.AttackingType == PokemonType.Ground);

        Assert.Equal(1, groundAnalysis.ImmunePokemonCount);
        Assert.Equal(0.0, groundAnalysis.TotalMultiplier);
    }
}
