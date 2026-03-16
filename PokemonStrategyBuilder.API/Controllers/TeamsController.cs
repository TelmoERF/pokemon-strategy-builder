using Microsoft.AspNetCore.Mvc;
using PokemonStrategyBuilder.API.Models;
using PokemonStrategyBuilder.Application.DTOs;
using PokemonStrategyBuilder.Application.Interfaces;

namespace PokemonStrategyBuilder.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    private readonly ITeamService _teamService;

    public TeamsController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTeamRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var dto = new CreateTeamRequestDto
            {
                Name = request.Name,
                PokemonNames = request.PokemonNames
            };

            var createdTeam = await _teamService.CreateAsync(dto, cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id = createdTeam.Id }, createdTeam);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var team = await _teamService.GetByIdAsync(id, cancellationToken);

        if (team is null)
        {
            return NotFound();
        }

        return Ok(team);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var teams = await _teamService.GetAllAsync(cancellationToken);
        return Ok(teams);
    }

    [HttpPost("{id:int}/analyze")]
    public async Task<IActionResult> Analyze(int id, CancellationToken cancellationToken)
    {
        var analysis = await _teamService.AnalyzeTeamAsync(id, cancellationToken);

        if (analysis is null)
        {
            return NotFound();
        }

        return Ok(analysis);
    }

    [HttpPut("{id:int}")]
public async Task<IActionResult> Update(int id, [FromBody] UpdateTeamRequest request, CancellationToken cancellationToken)
{
    try
    {
        var dto = new UpdateTeamRequestDto
        {
            Name = request.Name,
            PokemonNames = request.PokemonNames
        };

        var updatedTeam = await _teamService.UpdateAsync(id, dto, cancellationToken);

        if (updatedTeam is null)
        {
            return NotFound();
        }

        return Ok(updatedTeam);
    }
    catch (ArgumentException ex)
    {
        return BadRequest(new { ex.Message });
    }
    catch (InvalidOperationException ex)
    {
        return BadRequest(new { ex.Message });
    }
}

[HttpDelete("{id:int}")]
public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
{
    var deleted = await _teamService.DeleteAsync(id, cancellationToken);

    if (!deleted)
    {
        return NotFound();
    }

    return NoContent();
}

[HttpGet("{id:int}/rating")]
public async Task<IActionResult> GetRating(int id, CancellationToken cancellationToken)
{
    var rating = await _teamService.GetRatingAsync(id, cancellationToken);

    if (rating is null)
    {
        return NotFound();
    }

    return Ok(rating);
}

[HttpGet("{id:int}/offensive-coverage")]
public async Task<IActionResult> GetOffensiveCoverage(int id, CancellationToken cancellationToken)
{
    var coverage = await _teamService.GetOffensiveCoverageAsync(id, cancellationToken);

    if (coverage is null)
    {
        return NotFound();
    }

    return Ok(coverage);
}



}
