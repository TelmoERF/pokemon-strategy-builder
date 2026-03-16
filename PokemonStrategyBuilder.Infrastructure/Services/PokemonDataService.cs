using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using PokemonStrategyBuilder.Application.Interfaces;
using PokemonStrategyBuilder.Domain.Entities;
using PokemonStrategyBuilder.Domain.Enums;
using PokemonStrategyBuilder.Infrastructure.Clients;
using PokemonStrategyBuilder.Infrastructure.Models;

namespace PokemonStrategyBuilder.Infrastructure.Services;

public class PokemonDataService : IPokemonDataService
{
    private readonly PokeApiClient _pokeApiClient;
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<PokemonDataService> _logger;

    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(30);

    public PokemonDataService(
        PokeApiClient pokeApiClient,
        IMemoryCache memoryCache,
        ILogger<PokemonDataService> logger)
    {
        _pokeApiClient = pokeApiClient;
        _memoryCache = memoryCache;
        _logger = logger;
    }

    public async Task<Pokemon?> GetPokemonByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var normalizedName = name.Trim().ToLowerInvariant();
        var cacheKey = $"pokemon:{normalizedName}";

        if (_memoryCache.TryGetValue(cacheKey, out Pokemon? cachedPokemon))
        {
            _logger.LogInformation("Cache hit for Pokémon '{PokemonName}'", normalizedName);
            return cachedPokemon;
        }

        _logger.LogInformation("Cache miss for Pokémon '{PokemonName}'", normalizedName);

        var response = await _pokeApiClient.GetPokemonByNameAsync(normalizedName, cancellationToken);

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

        var pokemon = new Pokemon(
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

        _memoryCache.Set(cacheKey, pokemon, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = CacheDuration
        });

        return pokemon;
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