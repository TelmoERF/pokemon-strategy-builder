using PokemonStrategyBuilder.Domain.Entities;

namespace PokemonStrategyBuilder.Application.Interfaces;

public interface IPokemonRepository
{
    Task<Pokemon?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task AddAsync(Pokemon pokemon, CancellationToken cancellationToken = default);
}
