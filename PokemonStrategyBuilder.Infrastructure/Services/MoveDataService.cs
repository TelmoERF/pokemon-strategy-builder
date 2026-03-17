using PokemonStrategyBuilder.Application.Interfaces;
using PokemonStrategyBuilder.Domain.Entities;
using PokemonStrategyBuilder.Domain.Enums;
using PokemonStrategyBuilder.Infrastructure.Clients;

namespace PokemonStrategyBuilder.Infrastructure.Services;

public class MoveDataService : IMoveDataService
{
    private readonly PokeApiClient _pokeApiClient;
    private readonly IMoveRepository _moveRepository;

    public MoveDataService(PokeApiClient pokeApiClient, IMoveRepository moveRepository)
    {
        _pokeApiClient = pokeApiClient;
        _moveRepository = moveRepository;
    }

    public async Task<Move?> GetMoveByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var normalizedName = name.Trim().ToLowerInvariant();

        var existingMove = await _moveRepository.GetByNameAsync(normalizedName, cancellationToken);
        if (existingMove is not null)
        {
            return existingMove;
        }

        var response = await _pokeApiClient.GetMoveByNameAsync(normalizedName, cancellationToken);
        if (response is null)
        {
            return null;
        }

        var move = new Move(
            id: response.Id,
            name: response.Name,
            type: MapPokemonType(response.Type.Name),
            category: MapMoveCategory(response.Damage_Class.Name),
            power: response.Power,
            accuracy: response.Accuracy,
            pp: response.Pp);

        await _moveRepository.AddAsync(move, cancellationToken);

        return move;
    }

    private static PokemonType MapPokemonType(string apiTypeName)
    {
        return apiTypeName.ToLowerInvariant() switch
        {
            "normal" => PokemonType.Normal,
            "fire" => PokemonType.Fire,
            "water" => PokemonType.Water,
            "electric" => PokemonType.Electric,
            "grass" => PokemonType.Grass,
            "ice" => PokemonType.Ice,
            "fighting" => PokemonType.Fighting,
            "poison" => PokemonType.Poison,
            "ground" => PokemonType.Ground,
            "flying" => PokemonType.Flying,
            "psychic" => PokemonType.Psychic,
            "bug" => PokemonType.Bug,
            "rock" => PokemonType.Rock,
            "ghost" => PokemonType.Ghost,
            "dragon" => PokemonType.Dragon,
            "dark" => PokemonType.Dark,
            "steel" => PokemonType.Steel,
            "fairy" => PokemonType.Fairy,
            _ => throw new ArgumentOutOfRangeException(nameof(apiTypeName), apiTypeName, "Unknown Pokémon type.")
        };
    }

    private static MoveCategory MapMoveCategory(string apiCategoryName)
    {
        return apiCategoryName.ToLowerInvariant() switch
        {
            "physical" => MoveCategory.Physical,
            "special" => MoveCategory.Special,
            "status" => MoveCategory.Status,
            _ => throw new ArgumentOutOfRangeException(nameof(apiCategoryName), apiCategoryName, "Unknown move category.")
        };
    }
}
