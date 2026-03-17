namespace PokemonStrategyBuilder.Application.DTOs;

public class TeamDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<TeamPokemonDto> Pokemon { get; set; } = [];
}

public class TeamPokemonDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PrimaryType { get; set; } = string.Empty;
    public string? SecondaryType { get; set; }

    public string Nickname { get; set; } = string.Empty;
    public int Level { get; set; }
    public string Item { get; set; } = string.Empty;
    public string Ability { get; set; } = string.Empty;
    public string? TeraType { get; set; }
    public bool IsShiny { get; set; }
    public string Gender { get; set; } = string.Empty;

    public int HpEv { get; set; }
    public int AttackEv { get; set; }
    public int DefenseEv { get; set; }
    public int SpecialAttackEv { get; set; }
    public int SpecialDefenseEv { get; set; }
    public int SpeedEv { get; set; }

    public List<string> Moves { get; set; } = [];
}
