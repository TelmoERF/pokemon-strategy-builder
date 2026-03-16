using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonStrategyBuilder.Domain.Entities;

namespace PokemonStrategyBuilder.Infrastructure.Persistence.Configurations;

public class TeamPokemonConfiguration : IEntityTypeConfiguration<TeamPokemon>
{
    public void Configure(EntityTypeBuilder<TeamPokemon> builder)
    {
        builder.ToTable("TeamPokemon");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.TeamId)
            .IsRequired();

        builder.Property(x => x.PokemonId)
            .IsRequired();

        builder.HasOne(x => x.Pokemon)
            .WithMany()
            .HasForeignKey(x => x.PokemonId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}