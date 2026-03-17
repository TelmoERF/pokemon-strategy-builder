using PokemonStrategyBuilder.Domain.Enums;

namespace PokemonStrategyBuilder.Domain.Entities;

public class TeamPokemon
{
    public int Id { get; private set; }

    public int TeamId { get; private set; }
    public Team Team { get; private set; } = null!;

    public int PokemonId { get; private set; }
    public Pokemon Pokemon { get; private set; } = null!;

    public string Nickname { get; private set; } = string.Empty;
    public int Level { get; private set; }
    public string Item { get; private set; } = string.Empty;
    public string Ability { get; private set; } = string.Empty;
    public PokemonType? TeraType { get; private set; }
    public bool IsShiny { get; private set; }
    public PokemonGender Gender { get; private set; }

    public int HpEv { get; private set; }
    public int AttackEv { get; private set; }
    public int DefenseEv { get; private set; }
    public int SpecialAttackEv { get; private set; }
    public int SpecialDefenseEv { get; private set; }
    public int SpeedEv { get; private set; }

    private TeamPokemon()
    {
    }

    public TeamPokemon(
        int pokemonId,
        string nickname,
        int level,
        string item,
        string ability,
        PokemonType? teraType,
        bool isShiny,
        PokemonGender gender,
        int hpEv,
        int attackEv,
        int defenseEv,
        int specialAttackEv,
        int specialDefenseEv,
        int speedEv)
    {
        if (pokemonId <= 0)
        {
            throw new ArgumentException("PokemonId must be greater than 0.", nameof(pokemonId));
        }

        if (level < 1 || level > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(level), "Level must be between 1 and 100.");
        }

        ValidateEv(hpEv, nameof(hpEv));
        ValidateEv(attackEv, nameof(attackEv));
        ValidateEv(defenseEv, nameof(defenseEv));
        ValidateEv(specialAttackEv, nameof(specialAttackEv));
        ValidateEv(specialDefenseEv, nameof(specialDefenseEv));
        ValidateEv(speedEv, nameof(speedEv));

        var totalEvs = hpEv + attackEv + defenseEv + specialAttackEv + specialDefenseEv + speedEv;

        if (totalEvs > 510)
        {
            throw new ArgumentException("Total EVs cannot exceed 510.");
        }

        PokemonId = pokemonId;
        Nickname = nickname?.Trim() ?? string.Empty;
        Level = level;
        Item = item?.Trim() ?? string.Empty;
        Ability = ability?.Trim() ?? string.Empty;
        TeraType = teraType;
        IsShiny = isShiny;
        Gender = gender;
        HpEv = hpEv;
        AttackEv = attackEv;
        DefenseEv = defenseEv;
        SpecialAttackEv = specialAttackEv;
        SpecialDefenseEv = specialDefenseEv;
        SpeedEv = speedEv;
    }

    private static void ValidateEv(int value, string paramName)
    {
        if (value < 0 || value > 252)
        {
            throw new ArgumentOutOfRangeException(paramName, "Each EV must be between 0 and 252.");
        }
    }
}
