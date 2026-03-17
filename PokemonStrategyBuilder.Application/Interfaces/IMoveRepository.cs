using PokemonStrategyBuilder.Domain.Entities;

namespace PokemonStrategyBuilder.Application.Interfaces;

public interface IMoveRepository
{
    Task<Move?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task AddAsync(Move move, CancellationToken cancellationToken = default);
}
