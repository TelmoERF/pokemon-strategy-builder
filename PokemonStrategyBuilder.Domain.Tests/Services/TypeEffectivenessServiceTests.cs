using PokemonStrategyBuilder.Domain.Enums;
using PokemonStrategyBuilder.Domain.Services;

namespace PokemonStrategyBuilder.Domain.Tests.Services;

public class TypeEffectivenessServiceTests
{
    private readonly TypeEffectivenessService _service = new();

    [Fact]
    public void GetEffectiveness_ShouldReturnTwo_WhenFireAttacksGrass()
    {
        var result = _service.GetEffectiveness(PokemonType.Fire, PokemonType.Grass);

        Assert.Equal(2.0, result);
    }

    [Fact]
    public void GetEffectiveness_ShouldReturnZero_WhenElectricAttacksGround()
    {
        var result = _service.GetEffectiveness(PokemonType.Electric, PokemonType.Ground);

        Assert.Equal(0.0, result);
    }

    [Fact]
    public void GetEffectiveness_ShouldReturnOne_WhenNoSpecialInteractionExists()
    {
        var result = _service.GetEffectiveness(PokemonType.Normal, PokemonType.Electric);

        Assert.Equal(1.0, result);
    }

    [Fact]
    public void GetEffectiveness_ShouldReturnFour_WhenRockAttacksFireFlying()
    {
        var result = _service.GetEffectiveness(
            PokemonType.Rock,
            PokemonType.Fire,
            PokemonType.Flying);

        Assert.Equal(4.0, result);
    }

    [Fact]
    public void GetEffectiveness_ShouldReturnQuarter_WhenGrassAttacksFireFlying()
    {
        var result = _service.GetEffectiveness(
            PokemonType.Grass,
            PokemonType.Fire,
            PokemonType.Flying);

        Assert.Equal(0.25, result);
    }

    [Fact]
    public void GetEffectiveness_ShouldReturnZero_WhenOneTypeIsImmune()
    {
        var result = _service.GetEffectiveness(
            PokemonType.Normal,
            PokemonType.Ghost,
            PokemonType.Dark);

        Assert.Equal(0.0, result);
    }
}
