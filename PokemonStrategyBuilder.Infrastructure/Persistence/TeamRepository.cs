using Microsoft.EntityFrameworkCore;
using PokemonStrategyBuilder.Application.Interfaces;
using PokemonStrategyBuilder.Domain.Entities;

namespace PokemonStrategyBuilder.Infrastructure.Persistence;

public class TeamRepository : ITeamRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TeamRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Team> AddAsync(Team team, CancellationToken cancellationToken = default)
    {
        _dbContext.Teams.Add(team);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return team;
    }

    public async Task<Team?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Teams
            .Include(t => t.Pokemon)
                .ThenInclude(tp => tp.Pokemon)
            .Include(t => t.Pokemon)
                .ThenInclude(tp => tp.Moves)
                    .ThenInclude(tpm => tpm.Move)
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<List<Team>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Teams
            .Include(t => t.Pokemon)
                .ThenInclude(tp => tp.Pokemon)
            .Include(t => t.Pokemon)
                .ThenInclude(tp => tp.Moves)
                    .ThenInclude(tpm => tpm.Move)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Team team, CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Team team, CancellationToken cancellationToken = default)
    {
        _dbContext.Teams.Remove(team);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
