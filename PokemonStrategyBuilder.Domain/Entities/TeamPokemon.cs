namespace PokemonStrategyBuilder.Domain.Entities;

public class TeamPokemon
{
    public int Id { get; private set; }

    public int TeamId { get; private set; }
    public Team Team { get; private set; } = null!;

    public int PokemonId { get; private set; }
    public Pokemon Pokemon { get; private set; } = null!;

    private TeamPokemon()
    {
    }

    public TeamPokemon(int pokemonId)
    {
        PokemonId = pokemonId;
    }
}
