using PokemonStrategyBuilder.Application.DTOs;
using PokemonStrategyBuilder.Domain.Entities;

namespace PokemonStrategyBuilder.Application.Interfaces;

public interface ITeamRatingService
{
    TeamRatingDto Rate(int teamId, string teamName, IReadOnlyCollection<Pokemon> pokemon, IReadOnlyCollection<TypeWeaknessDto> weaknesses);
}
