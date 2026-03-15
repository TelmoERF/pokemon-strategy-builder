using PokemonStrategyBuilder.Domain.Enums;

namespace PokemonStrategyBuilder.Application.DTOs;

public class TypeWeaknessDto
{
    public PokemonType AttackingType { get; set; }
    public int WeakPokemonCount { get; set; }
    public int ResistantPokemonCount { get; set; }
    public int ImmunePokemonCount { get; set; }
    public double TotalMultiplier { get; set; }
}
