using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonStrategyBuilder.Domain.Entities;

namespace PokemonStrategyBuilder.Infrastructure.Persistence.Configurations;

public class PokemonConfiguration : IEntityTypeConfiguration<Pokemon>
{
    public void Configure(EntityTypeBuilder<Pokemon> builder)
    {
        builder.ToTable("Pokemon");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.PrimaryType)
            .IsRequired();

        builder.Property(x => x.SecondaryType);

        builder.Property(x => x.Hp).IsRequired();
        builder.Property(x => x.Attack).IsRequired();
        builder.Property(x => x.Defense).IsRequired();
        builder.Property(x => x.SpecialAttack).IsRequired();
        builder.Property(x => x.SpecialDefense).IsRequired();
        builder.Property(x => x.Speed).IsRequired();
    }
}