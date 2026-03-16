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
        SetName(name);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Team name cannot be empty.", nameof(name));
        }

        Name = name.Trim();
    }

    public void AddPokemon(Pokemon pokemon)
    {
        if (_pokemon.Count >= 6)
        {
            throw new InvalidOperationException("A team cannot have more than 6 Pokémon.");
        }

        if (_pokemon.Any(x => x.PokemonId == pokemon.Id))
        {
            throw new InvalidOperationException("This Pokémon is already in the team.");
        }

        _pokemon.Add(new TeamPokemon(pokemon.Id));
    }

    public void ReplacePokemon(IEnumerable<Pokemon> pokemon)
    {
        var pokemonList = pokemon.ToList();

        if (pokemonList.Count == 0)
        {
            throw new InvalidOperationException("A team must contain at least one Pokémon.");
        }

        if (pokemonList.Count > 6)
        {
            throw new InvalidOperationException("A team cannot have more than 6 Pokémon.");
        }

        if (pokemonList.Select(x => x.Id).Distinct().Count() != pokemonList.Count)
        {
            throw new InvalidOperationException("A team cannot contain duplicate Pokémon.");
        }

        _pokemon.Clear();

        foreach (var teamPokemon in pokemonList)
        {
            _pokemon.Add(new TeamPokemon(teamPokemon.Id));
        }
    }
}
