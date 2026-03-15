using PokemonStrategyBuilder.Domain.Enums;

namespace PokemonStrategyBuilder.Domain.Entities;

public class Pokemon
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

    public PokemonType PrimaryType { get; private set; }
    public PokemonType? SecondaryType { get; private set; }

    public int Hp { get; private set; }
    public int Attack { get; private set; }
    public int Defense { get; private set; }
    public int SpecialAttack { get; private set; }
    public int SpecialDefense { get; private set; }
    public int Speed { get; private set; }

    private Pokemon()
    {
    }

    public Pokemon(
        int id,
        string name,
        PokemonType primaryType,
        PokemonType? secondaryType,
        int hp,
        int attack,
        int defense,
        int specialAttack,
        int specialDefense,
        int speed)
    {
        Id = id;
        Name = name;
        PrimaryType = primaryType;
        SecondaryType = secondaryType;
        Hp = hp;
        Attack = attack;
        Defense = defense;
        SpecialAttack = specialAttack;
        SpecialDefense = specialDefense;
        Speed = speed;
    }
}
