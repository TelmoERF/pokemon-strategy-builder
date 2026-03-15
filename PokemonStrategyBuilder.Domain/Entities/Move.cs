using PokemonStrategyBuilder.Domain.Enums;

namespace PokemonStrategyBuilder.Domain.Entities;

public class Move
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public PokemonType Type { get; private set; }
    public MoveCategory Category { get; private set; }
    public int? Power { get; private set; }
    public int? Accuracy { get; private set; }
    public int Pp { get; private set; }

    private Move()
    {
    }

    public Move(
        int id,
        string name,
        PokemonType type,
        MoveCategory category,
        int? power,
        int? accuracy,
        int pp)
    {
        Id = id;
        Name = name;
        Type = type;
        Category = category;
        Power = power;
        Accuracy = accuracy;
        Pp = pp;
    }
}
