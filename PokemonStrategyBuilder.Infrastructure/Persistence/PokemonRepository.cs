using Microsoft.EntityFrameworkCore;
using PokemonStrategyBuilder.Application.Interfaces;
using PokemonStrategyBuilder.Domain.Entities;

namespace PokemonStrategyBuilder.Infrastructure.Persistence;

public class PokemonRepository : IPokemonRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PokemonRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Pokemon?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var normalizedName = name.Trim().ToLower();

        return await _dbContext.Pokemon
            .FirstOrDefaultAsync(p => p.Name.ToLower() == normalizedName, cancellationToken);
    }

    public async Task AddAsync(Pokemon pokemon, CancellationToken cancellationToken = default)
    {
        await _dbContext.Pokemon.AddAsync(pokemon, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}