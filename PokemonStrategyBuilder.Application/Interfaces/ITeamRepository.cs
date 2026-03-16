using PokemonStrategyBuilder.Domain.Entities;

namespace PokemonStrategyBuilder.Application.Interfaces;

public interface ITeamRepository
{
    Task<Team> AddAsync(Team team, CancellationToken cancellationToken = default);
    Task<Team?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<List<Team>> GetAllAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(Team team, CancellationToken cancellationToken = default);
    Task DeleteAsync(Team team, CancellationToken cancellationToken = default);
}
