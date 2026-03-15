using PokemonStrategyBuilder.Domain.Entities;

namespace PokemonStrategyBuilder.Application.Interfaces;

public interface IPokemonDataService
{
    Task<Pokemon?> GetPokemonByNameAsync(string name, CancellationToken cancellationToken = default);
}
