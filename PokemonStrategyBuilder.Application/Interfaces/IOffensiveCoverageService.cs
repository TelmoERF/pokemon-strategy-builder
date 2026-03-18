using PokemonStrategyBuilder.Application.DTOs;
using PokemonStrategyBuilder.Domain.Entities;

namespace PokemonStrategyBuilder.Application.Interfaces;

public interface IOffensiveCoverageService
{
    OffensiveCoverageDto Analyze(int teamId, string teamName, IReadOnlyCollection<Pokemon> pokemon);
    OffensiveCoverageDto AnalyzeFromTeamSlots(int teamId, string teamName, IReadOnlyCollection<TeamPokemon> teamPokemon);
}
