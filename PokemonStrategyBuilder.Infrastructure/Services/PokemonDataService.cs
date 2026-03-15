using PokemonStrategyBuilder.Application.Interfaces;
using PokemonStrategyBuilder.Domain.Entities;
using PokemonStrategyBuilder.Domain.Enums;
using PokemonStrategyBuilder.Infrastructure.Clients;
using PokemonStrategyBuilder.Infrastructure.Models;

namespace PokemonStrategyBuilder.Infrastructure.Services;

public class PokemonDataService : IPokemonDataService
{
    private readonly PokeApiClient _pokeApiClient;

    public PokemonDataService(PokeApiClient pokeApiClient)
    {
        _pokeApiClient = pokeApiClient;
    }

    public async Task<Pokemon?> GetPokemonByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var response = await _pokeApiClient.GetPokemonByNameAsync(name, cancellationToken);

        if (response is null)
        {
            return null;
        }

        var orderedTypes = response.Types
            .OrderBy(t => t.Slot)
            .ToList();

        var primaryType = MapPokemonType(orderedTypes[0].Type.Name);
        PokemonType? secondaryType = orderedTypes.Count > 1
            ? MapPokemonType(orderedTypes[1].Type.Name)
            : null;

        return new Pokemon(
            id: response.Id,
            name: response.Name,
            primaryType: primaryType,
            secondaryType: secondaryType,
            hp: GetStat(response, "hp"),
            attack: GetStat(response, "attack"),
            defense: GetStat(response, "defense"),
            specialAttack: GetStat(response, "special-attack"),
            specialDefense: GetStat(response, "special-defense"),
            speed: GetStat(response, "speed"));
    }

    private static int GetStat(PokeApiPokemonResponse response, string statName)
    {
        return response.Stats.FirstOrDefault(s => s.Stat.Name == statName)?.Base_Stat ?? 0;
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
}
