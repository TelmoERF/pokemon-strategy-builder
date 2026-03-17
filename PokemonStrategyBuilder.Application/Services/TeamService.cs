using PokemonStrategyBuilder.Application.DTOs;
using PokemonStrategyBuilder.Application.Interfaces;
using PokemonStrategyBuilder.Domain.Entities;

namespace PokemonStrategyBuilder.Application.Services;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;
    private readonly IPokemonDataService _pokemonDataService;
    private readonly ITeamWeaknessAnalyzerService _analyzer;
    private readonly ITeamRatingService _teamRatingService;
    private readonly IOffensiveCoverageService _offensiveCoverageService;
    private readonly IMoveDataService _moveDataService;

    public TeamService(
        ITeamRepository teamRepository,
        IPokemonDataService pokemonDataService,
        ITeamWeaknessAnalyzerService analyzer,
        ITeamRatingService teamRatingService,
        IOffensiveCoverageService offensiveCoverageService,
        IMoveDataService moveDataService)
    {
        _teamRepository = teamRepository;
        _pokemonDataService = pokemonDataService;
        _analyzer = analyzer;
        _teamRatingService = teamRatingService;
        _offensiveCoverageService = offensiveCoverageService;
        _moveDataService = moveDataService;
    }

    public async Task<TeamDto> CreateAsync(CreateTeamRequestDto request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Team name is required.");
        }

        if (request.Pokemon is null || request.Pokemon.Count == 0)
        {
            throw new ArgumentException("At least one Pokémon must be provided.");
        }

        if (request.Pokemon.Count > 6)
        {
            throw new ArgumentException("A team cannot contain more than 6 Pokémon.");
        }

        var team = new Team(request.Name);
        var notFoundPokemon = new List<string>();
        var notFoundMoves = new List<string>();

        foreach (var slot in request.Pokemon)
        {
            var pokemon = await _pokemonDataService.GetPokemonByNameAsync(slot.SpeciesName, cancellationToken);

            if (pokemon is null)
            {
                notFoundPokemon.Add(slot.SpeciesName);
                continue;
            }

            var createdSlot = team.AddPokemon(
                pokemon: pokemon,
                nickname: slot.Nickname,
                level: slot.Level,
                item: slot.Item,
                ability: slot.Ability,
                teraType: slot.TeraType,
                isShiny: slot.IsShiny,
                gender: slot.Gender,
                hpEv: slot.HpEv,
                attackEv: slot.AttackEv,
                defenseEv: slot.DefenseEv,
                specialAttackEv: slot.SpecialAttackEv,
                specialDefenseEv: slot.SpecialDefenseEv,
                speedEv: slot.SpeedEv);

            var resolvedMoves = new List<Move>();

            foreach (var moveName in slot.Moves)
            {
                var move = await _moveDataService.GetMoveByNameAsync(moveName, cancellationToken);

                if (move is null)
                {
                    notFoundMoves.Add(moveName);
                    continue;
                }

                resolvedMoves.Add(move);
            }

            createdSlot.SetMoves(resolvedMoves);
        }

        if (notFoundPokemon.Count > 0)
        {
            throw new InvalidOperationException(
                $"The following Pokémon could not be found: {string.Join(", ", notFoundPokemon)}");
        }

        if (notFoundMoves.Count > 0)
        {
            throw new InvalidOperationException(
                $"The following moves could not be found: {string.Join(", ", notFoundMoves)}");
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

    public async Task<TeamDto?> UpdateAsync(int id, UpdateTeamRequestDto request, CancellationToken cancellationToken = default)
    {
        var team = await _teamRepository.GetByIdAsync(id, cancellationToken);

        if (team is null)
        {
            return null;
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Team name is required.");
        }

        if (request.Pokemon is null || request.Pokemon.Count == 0)
        {
            throw new ArgumentException("At least one Pokémon must be provided.");
        }

        if (request.Pokemon.Count > 6)
        {
            throw new ArgumentException("A team cannot contain more than 6 Pokémon.");
        }

        var resolvedSlots = new List<(Pokemon Pokemon, TeamPokemon Slot)>();
        var notFoundPokemon = new List<string>();
        var notFoundMoves = new List<string>();

        foreach (var slot in request.Pokemon)
        {
            var pokemon = await _pokemonDataService.GetPokemonByNameAsync(slot.SpeciesName, cancellationToken);

            if (pokemon is null)
            {
                notFoundPokemon.Add(slot.SpeciesName);
                continue;
            }

            var teamPokemonSlot = new TeamPokemon(
                pokemonId: pokemon.Id,
                nickname: slot.Nickname,
                level: slot.Level,
                item: slot.Item,
                ability: slot.Ability,
                teraType: slot.TeraType,
                isShiny: slot.IsShiny,
                gender: slot.Gender,
                hpEv: slot.HpEv,
                attackEv: slot.AttackEv,
                defenseEv: slot.DefenseEv,
                specialAttackEv: slot.SpecialAttackEv,
                specialDefenseEv: slot.SpecialDefenseEv,
                speedEv: slot.SpeedEv);

            var resolvedMoves = new List<Move>();

            foreach (var moveName in slot.Moves)
            {
                var move = await _moveDataService.GetMoveByNameAsync(moveName, cancellationToken);

                if (move is null)
                {
                    notFoundMoves.Add(moveName);
                    continue;
                }

                resolvedMoves.Add(move);
            }

            teamPokemonSlot.SetMoves(resolvedMoves);

            resolvedSlots.Add((pokemon, teamPokemonSlot));
        }

        if (notFoundPokemon.Count > 0)
        {
            throw new InvalidOperationException(
                $"The following Pokémon could not be found: {string.Join(", ", notFoundPokemon)}");
        }

        if (notFoundMoves.Count > 0)
        {
            throw new InvalidOperationException(
                $"The following moves could not be found: {string.Join(", ", notFoundMoves)}");
        }

        team.SetName(request.Name);
        team.ReplacePokemon(resolvedSlots);

        await _teamRepository.UpdateAsync(team, cancellationToken);

        var updatedTeam = await _teamRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new InvalidOperationException("Updated team could not be reloaded.");

        return MapToDto(updatedTeam);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var team = await _teamRepository.GetByIdAsync(id, cancellationToken);

        if (team is null)
        {
            return false;
        }

        await _teamRepository.DeleteAsync(team, cancellationToken);
        return true;
    }

    public async Task<TeamRatingDto?> GetRatingAsync(int id, CancellationToken cancellationToken = default)
    {
        var team = await _teamRepository.GetByIdAsync(id, cancellationToken);

        if (team is null)
        {
            return null;
        }

        var pokemon = team.Pokemon
            .Select(tp => tp.Pokemon)
            .ToList();

        var weaknesses = _analyzer.Analyze(pokemon);

        return _teamRatingService.Rate(team.Id, team.Name, pokemon, weaknesses);
    }

    public async Task<OffensiveCoverageDto?> GetOffensiveCoverageAsync(int id, CancellationToken cancellationToken = default)
    {
        var team = await _teamRepository.GetByIdAsync(id, cancellationToken);

        if (team is null)
        {
            return null;
        }

        var pokemon = team.Pokemon
            .Select(tp => tp.Pokemon)
            .ToList();

        return _offensiveCoverageService.Analyze(team.Id, team.Name, pokemon);
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
                SecondaryType = tp.Pokemon.SecondaryType?.ToString(),
                Nickname = tp.Nickname,
                Level = tp.Level,
                Item = tp.Item,
                Ability = tp.Ability,
                TeraType = tp.TeraType?.ToString(),
                IsShiny = tp.IsShiny,
                Gender = tp.Gender.ToString(),
                HpEv = tp.HpEv,
                AttackEv = tp.AttackEv,
                DefenseEv = tp.DefenseEv,
                SpecialAttackEv = tp.SpecialAttackEv,
                SpecialDefenseEv = tp.SpecialDefenseEv,
                SpeedEv = tp.SpeedEv,
                Moves = tp.Moves
                    .OrderBy(m => m.Slot)
                    .Select(m => m.Move.Name)
                    .ToList()
            }).ToList()
        };
    }
}
