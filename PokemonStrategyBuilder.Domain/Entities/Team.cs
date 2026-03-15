namespace PokemonStrategyBuilder.Domain.Entities;

public class Team
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

    private readonly List<TeamPokemon> _pokemon = [];
    public IReadOnlyCollection<TeamPokemon> Pokemon => _pokemon.AsReadOnly();

    private Team()
    {
    }

    public Team(string name)
    {
        Name = name;
    }

    public void AddPokemon(int pokemonId)
    {
        if (_pokemon.Count >= 6)
        {
            throw new InvalidOperationException("A team cannot have more than 6 Pokémon.");
        }

        _pokemon.Add(new TeamPokemon(pokemonId));
    }
}
