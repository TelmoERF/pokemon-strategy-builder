namespace PokemonStrategyBuilder.Infrastructure.Models;

public class PokeApiPokemonResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<PokeApiTypeSlot> Types { get; set; } = [];
    public List<PokeApiStatSlot> Stats { get; set; } = [];
}

public class PokeApiTypeSlot
{
    public int Slot { get; set; }
    public PokeApiNamedApiResource Type { get; set; } = new();
}

public class PokeApiStatSlot
{
    public int Base_Stat { get; set; }
    public int Effort { get; set; }
    public PokeApiNamedApiResource Stat { get; set; } = new();
}

public class PokeApiNamedApiResource
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}
