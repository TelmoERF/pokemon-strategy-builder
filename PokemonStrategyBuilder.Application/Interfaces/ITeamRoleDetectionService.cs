using PokemonStrategyBuilder.Application.DTOs;
using PokemonStrategyBuilder.Domain.Entities;

namespace PokemonStrategyBuilder.Application.Interfaces;

public interface ITeamRoleDetectionService
{
    TeamRoleAnalysisDto Analyze(int teamId, string teamName, IReadOnlyCollection<TeamPokemon> teamPokemon);
}
