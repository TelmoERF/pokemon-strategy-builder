using PokemonStrategyBuilder.Application.DTOs;

namespace PokemonStrategyBuilder.Application.Interfaces;

public interface ITeamService
{
    Task<TeamDto> CreateAsync(CreateTeamRequestDto request, CancellationToken cancellationToken = default);
    Task<TeamDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<List<TeamDto>> GetAllAsync(CancellationToken cancellationToken = default);
}
