using PokemonStrategyBuilder.Application.DTOs;

namespace PokemonStrategyBuilder.Application.Interfaces;

public interface ITeamService
{
    Task<TeamDto> CreateAsync(CreateTeamRequestDto request, CancellationToken cancellationToken = default);
    Task<TeamDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<List<TeamDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<TypeWeaknessDto>?> AnalyzeTeamAsync(int teamId, CancellationToken cancellationToken = default);
    Task<TeamDto?> UpdateAsync(int id, UpdateTeamRequestDto request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<TeamRatingDto?> GetRatingAsync(int id, CancellationToken cancellationToken = default);
}
