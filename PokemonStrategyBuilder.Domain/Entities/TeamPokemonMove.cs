namespace PokemonStrategyBuilder.Domain.Entities;

public class TeamPokemonMove
{
    public int Id { get; private set; }

    public int TeamPokemonId { get; private set; }
    public TeamPokemon TeamPokemon { get; private set; } = null!;

    public int MoveId { get; private set; }
    public Move Move { get; private set; } = null!;

    public int Slot { get; private set; }

    private TeamPokemonMove()
    {
    }

    public TeamPokemonMove(int moveId, int slot)
    {
        if (moveId <= 0)
        {
            throw new ArgumentException("MoveId must be greater than 0.", nameof(moveId));
        }

        if (slot < 1 || slot > 4)
        {
            throw new ArgumentOutOfRangeException(nameof(slot), "Move slot must be between 1 and 4.");
        }

        MoveId = moveId;
        Slot = slot;
    }
}
