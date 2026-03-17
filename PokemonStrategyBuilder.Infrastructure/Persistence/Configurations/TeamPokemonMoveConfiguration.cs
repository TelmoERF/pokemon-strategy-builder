using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonStrategyBuilder.Domain.Entities;

namespace PokemonStrategyBuilder.Infrastructure.Persistence.Configurations;

public class TeamPokemonMoveConfiguration : IEntityTypeConfiguration<TeamPokemonMove>
{
    public void Configure(EntityTypeBuilder<TeamPokemonMove> builder)
    {
        builder.ToTable("TeamPokemonMoves");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.TeamPokemonId)
            .IsRequired();

        builder.Property(x => x.MoveId)
            .IsRequired();

        builder.Property(x => x.Slot)
            .IsRequired();

        builder.HasOne(x => x.Move)
            .WithMany()
            .HasForeignKey(x => x.MoveId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.TeamPokemon)
            .WithMany(x => x.Moves)
            .HasForeignKey(x => x.TeamPokemonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
