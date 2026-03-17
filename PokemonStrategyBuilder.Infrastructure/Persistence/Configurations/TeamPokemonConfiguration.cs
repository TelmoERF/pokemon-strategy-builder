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

        builder.Property(x => x.Nickname)
            .HasMaxLength(100);

        builder.Property(x => x.Level)
            .IsRequired();

        builder.Property(x => x.Item)
            .HasMaxLength(100);

        builder.Property(x => x.Ability)
            .HasMaxLength(100);

        builder.Property(x => x.TeraType);

        builder.Property(x => x.IsShiny)
            .IsRequired();

        builder.Property(x => x.Gender)
            .IsRequired();

        builder.Property(x => x.HpEv)
            .IsRequired();

        builder.Property(x => x.AttackEv)
            .IsRequired();

        builder.Property(x => x.DefenseEv)
            .IsRequired();

        builder.Property(x => x.SpecialAttackEv)
            .IsRequired();

        builder.Property(x => x.SpecialDefenseEv)
            .IsRequired();

        builder.Property(x => x.SpeedEv)
            .IsRequired();

        builder.Navigation(x => x.Moves)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasOne(x => x.Pokemon)
            .WithMany()
            .HasForeignKey(x => x.PokemonId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
