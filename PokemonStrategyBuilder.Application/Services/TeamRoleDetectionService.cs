using PokemonStrategyBuilder.Application.DTOs;
using PokemonStrategyBuilder.Application.Interfaces;
using PokemonStrategyBuilder.Domain.Entities;
using PokemonStrategyBuilder.Domain.Enums;

namespace PokemonStrategyBuilder.Application.Services;

public class TeamRoleDetectionService : ITeamRoleDetectionService
{
    private static readonly HashSet<string> PivotMoves =
    [
        "u-turn",
        "volt-switch",
        "flip-turn",
        "parting-shot",
        "teleport"
    ];

    private static readonly HashSet<string> SupportMoves =
    [
        "wish",
        "protect",
        "heal-bell",
        "aromatherapy",
        "thunder-wave",
        "will-o-wisp",
        "toxic",
        "defog",
        "rapid-spin",
        "tailwind",
        "light-screen",
        "reflect",
        "roost",
        "recover",
        "soft-boiled",
        "moonlight",
        "morning-sun"
    ];

    private static readonly HashSet<string> SetupMoves =
    [
        "swords-dance",
        "nasty-plot",
        "dragon-dance",
        "calm-mind",
        "bulk-up",
        "agility",
        "quiver-dance",
        "shell-smash",
        "iron-defense",
        "amnesia"
    ];

    public TeamRoleAnalysisDto Analyze(int teamId, string teamName, IReadOnlyCollection<TeamPokemon> teamPokemon)
    {
        var results = teamPokemon.Select(AnalyzePokemon).ToList();

        return new TeamRoleAnalysisDto
        {
            TeamId = teamId,
            TeamName = teamName,
            PokemonRoles = results
        };
    }

    private static TeamPokemonRoleDto AnalyzePokemon(TeamPokemon slot)
    {
        var roles = new HashSet<TeamPokemonRole>();

        var moveList = slot.Moves
            .OrderBy(m => m.Slot)
            .Select(m => m.Move)
            .ToList();

        var physicalMoves = moveList.Count(m => m.Category == MoveCategory.Physical);
        var specialMoves = moveList.Count(m => m.Category == MoveCategory.Special);
        var statusMoves = moveList.Count(m => m.Category == MoveCategory.Status);

        var moveNames = moveList
            .Select(m => m.Name.ToLowerInvariant())
            .ToHashSet();

        var attackInvestment = slot.AttackEv;
        var specialAttackInvestment = slot.SpecialAttackEv;
        var defenseInvestment = slot.DefenseEv;
        var specialDefenseInvestment = slot.SpecialDefenseEv;
        var speedInvestment = slot.SpeedEv;
        var hpInvestment = slot.HpEv;

        if (physicalMoves >= 2 && attackInvestment >= 200 && speedInvestment >= 200)
        {
            roles.Add(TeamPokemonRole.PhysicalSweeper);
        }

        if (specialMoves >= 2 && specialAttackInvestment >= 200 && speedInvestment >= 200)
        {
            roles.Add(TeamPokemonRole.SpecialSweeper);
        }

        if (physicalMoves >= 2 && attackInvestment >= 200 && speedInvestment < 200)
        {
            roles.Add(TeamPokemonRole.Wallbreaker);
        }

        if (hpInvestment >= 200 && defenseInvestment >= 150)
        {
            roles.Add(TeamPokemonRole.PhysicalWall);
        }

        if (hpInvestment >= 200 && specialDefenseInvestment >= 150)
        {
            roles.Add(TeamPokemonRole.SpecialWall);
        }

        if (moveNames.Any(PivotMoves.Contains))
        {
            roles.Add(TeamPokemonRole.Pivot);
        }

        if (moveNames.Any(SupportMoves.Contains) || statusMoves >= 2)
        {
            roles.Add(TeamPokemonRole.Support);
        }

        if (moveNames.Any(SetupMoves.Contains))
        {
            roles.Add(TeamPokemonRole.SetupSweeper);
        }

        if (roles.Count == 0)
        {
            if (attackInvestment >= specialAttackInvestment)
            {
                roles.Add(TeamPokemonRole.PhysicalSweeper);
            }
            else
            {
                roles.Add(TeamPokemonRole.SpecialSweeper);
            }
        }

        return new TeamPokemonRoleDto
        {
            PokemonName = slot.Pokemon.Name,
            Nickname = slot.Nickname,
            Roles = roles.Select(r => r.ToString()).OrderBy(r => r).ToList()
        };
    }
}
