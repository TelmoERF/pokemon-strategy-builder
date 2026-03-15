using PokemonStrategyBuilder.Application.DTOs;
using PokemonStrategyBuilder.Domain.Entities;

namespace PokemonStrategyBuilder.Application.Interfaces;

public interface ITeamWeaknessAnalyzerService
{
    IReadOnlyCollection<TypeWeaknessDto> Analyze(IEnumerable<Pokemon> team);
}
