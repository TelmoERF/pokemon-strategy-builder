using PokemonStrategyBuilder.Domain.Entities;

namespace PokemonStrategyBuilder.Application.Interfaces;

public interface IMoveDataService
{
    Task<Move?> GetMoveByNameAsync(string name, CancellationToken cancellationToken = default);
}
