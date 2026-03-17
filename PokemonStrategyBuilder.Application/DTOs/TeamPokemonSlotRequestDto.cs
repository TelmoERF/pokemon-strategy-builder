using PokemonStrategyBuilder.Domain.Enums;

namespace PokemonStrategyBuilder.Application.DTOs;

public class TeamPokemonSlotRequestDto
{
    public string SpeciesName { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
    public int Level { get; set; } = 100;
    public string Item { get; set; } = string.Empty;
    public string Ability { get; set; } = string.Empty;
    public PokemonType? TeraType { get; set; }
    public bool IsShiny { get; set; }
    public PokemonGender Gender { get; set; } = PokemonGender.Unknown;

    public int HpEv { get; set; }
    public int AttackEv { get; set; }
    public int DefenseEv { get; set; }
    public int SpecialAttackEv { get; set; }
    public int SpecialDefenseEv { get; set; }
    public int SpeedEv { get; set; }

    public List<string> Moves { get; set; } = [];
}
