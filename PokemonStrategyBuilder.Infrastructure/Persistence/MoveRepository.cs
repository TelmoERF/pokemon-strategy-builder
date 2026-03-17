using Microsoft.EntityFrameworkCore;
using PokemonStrategyBuilder.Application.Interfaces;
using PokemonStrategyBuilder.Domain.Entities;

namespace PokemonStrategyBuilder.Infrastructure.Persistence;

public class MoveRepository : IMoveRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MoveRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Move?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var normalizedName = name.Trim().ToLower();

        return await _dbContext.Moves
            .FirstOrDefaultAsync(m => m.Name.ToLower() == normalizedName, cancellationToken);
    }

    public async Task AddAsync(Move move, CancellationToken cancellationToken = default)
    {
        await _dbContext.Moves.AddAsync(move, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
