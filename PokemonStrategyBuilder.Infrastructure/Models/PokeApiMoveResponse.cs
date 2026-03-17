namespace PokemonStrategyBuilder.Infrastructure.Models;

public class PokeApiMoveResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? Power { get; set; }
    public int? Accuracy { get; set; }
    public int Pp { get; set; }
    public PokeApiNamedApiResource Type { get; set; } = new();
    public PokeApiNamedApiResource Damage_Class { get; set; } = new();
}
