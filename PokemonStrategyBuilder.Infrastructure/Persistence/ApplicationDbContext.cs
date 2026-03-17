using Microsoft.EntityFrameworkCore;
using PokemonStrategyBuilder.Domain.Entities;

namespace PokemonStrategyBuilder.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<Pokemon> Pokemon => Set<Pokemon>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<TeamPokemon> TeamPokemon => Set<TeamPokemon>();
    public DbSet<Move> Moves => Set<Move>();
    public DbSet<TeamPokemonMove> TeamPokemonMoves => Set<TeamPokemonMove>();


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
