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
}
