using System.Net;
using System.Net.Http.Json;
using PokemonStrategyBuilder.Infrastructure.Models;

namespace PokemonStrategyBuilder.Infrastructure.Clients;

public class PokeApiClient
{
    private readonly HttpClient _httpClient;

    public PokeApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PokeApiPokemonResponse?> GetPokemonByNameAsync(
        string name,
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(
            $"pokemon/{name.ToLowerInvariant()}",
            cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<PokeApiPokemonResponse>(cancellationToken: cancellationToken);
    }

    public async Task<PokeApiMoveResponse?> GetMoveByNameAsync(
    string name,
    CancellationToken cancellationToken = default)
{
    var response = await _httpClient.GetAsync(
        $"move/{name.ToLowerInvariant()}",
        cancellationToken);

    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
    {
        return null;
    }

    response.EnsureSuccessStatusCode();

    return await response.Content.ReadFromJsonAsync<PokeApiMoveResponse>(cancellationToken: cancellationToken);
}

}