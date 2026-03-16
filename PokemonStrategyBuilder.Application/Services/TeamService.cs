using PokemonStrategyBuilder.Application.DTOs;
using PokemonStrategyBuilder.Application.Interfaces;
using PokemonStrategyBuilder.Domain.Entities;

namespace PokemonStrategyBuilder.Application.Services;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;
    private readonly IPokemonDataService _pokemonDataService;
    private readonly ITeamWeaknessAnalyzerService _analyzer;

    public TeamService(
        ITeamRepository teamRepository,
        IPokemonDataService pokemonDataService,
        ITeamWeaknessAnalyzerService analyzer)
    {
        _teamRepository = teamRepository;
        _pokemonDataService = pokemonDataService;
        _analyzer = analyzer;
    }

    public async Task<TeamDto> CreateAsync(CreateTeamRequestDto request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Team name is required.");
        }

        if (request.PokemonNames is null || request.PokemonNames.Count == 0)
        {
            throw new ArgumentException("At least one Pokémon name must be provided.");
        }

        if (request.PokemonNames.Count > 6)
        {
            throw new ArgumentException("A team cannot contain more than 6 Pokémon.");
        }

        var team = new Team(request.Name);
        var notFoundPokemon = new List<string>();

        foreach (var pokemonName in request.PokemonNames)
        {
            var pokemon = await _pokemonDataService.GetPokemonByNameAsync(pokemonName, cancellationToken);

            if (pokemon is null)
            {
                notFoundPokemon.Add(pokemonName);
                continue;
            }

            team.AddPokemon(pokemon);
        }

        if (notFoundPokemon.Count > 0)
        {
            throw new InvalidOperationException(
                $"The following Pokémon could not be found: {string.Join(", ", notFoundPokemon)}");
        }

        var savedTeam = await _teamRepository.AddAsync(team, cancellationToken);

        var loadedTeam = await _teamRepository.GetByIdAsync(savedTeam.Id, cancellationToken)
            ?? throw new InvalidOperationException("Saved team could not be reloaded.");

        return MapToDto(loadedTeam);
    }

    public async Task<TeamDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var team = await _teamRepository.GetByIdAsync(id, cancellationToken);
        return team is null ? null : MapToDto(team);
    }

    public async Task<List<TeamDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var teams = await _teamRepository.GetAllAsync(cancellationToken);
        return teams.Select(MapToDto).ToList();
    }

    public async Task<IReadOnlyCollection<TypeWeaknessDto>?> AnalyzeTeamAsync(
    int teamId,
    CancellationToken cancellationToken = default)
    {
        var team = await _teamRepository.GetByIdAsync(teamId, cancellationToken);

        if (team is null)
        {
            return null;
        }

        var pokemon = team.Pokemon
            .Select(tp => tp.Pokemon)
            .ToList();

        return _analyzer.Analyze(pokemon);
    }

    private static TeamDto MapToDto(Team team)
    {
        return new TeamDto
        {
            Id = team.Id,
            Name = team.Name,
            Pokemon = team.Pokemon.Select(tp => new TeamPokemonDto
            {
                Id = tp.Pokemon.Id,
                Name = tp.Pokemon.Name,
                PrimaryType = tp.Pokemon.PrimaryType.ToString(),
                SecondaryType = tp.Pokemon.SecondaryType?.ToString()
            }).ToList()
        };
    }
}