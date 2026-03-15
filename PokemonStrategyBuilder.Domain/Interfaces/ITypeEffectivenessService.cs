using PokemonStrategyBuilder.Domain.Enums;

namespace PokemonStrategyBuilder.Domain.Interfaces;

public interface ITypeEffectivenessService
{
    double GetEffectiveness(PokemonType attackingType, PokemonType defendingType);
    double GetEffectiveness(
        PokemonType attackingType,
        PokemonType defendingPrimaryType,
        PokemonType? defendingSecondaryType);
}
