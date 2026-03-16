namespace PokemonStrategyBuilder.Application.DTOs;

public class TeamRatingDto
{
    public int TeamId { get; set; }
    public string TeamName { get; set; } = string.Empty;
    public int OverallScore { get; set; }
    public int WeaknessScore { get; set; }
    public int ResistanceScore { get; set; }
    public int CompletenessScore { get; set; }
    public List<string> Summary { get; set; } = [];
}
