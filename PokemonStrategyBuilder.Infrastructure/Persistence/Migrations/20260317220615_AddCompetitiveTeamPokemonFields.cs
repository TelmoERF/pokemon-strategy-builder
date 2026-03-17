using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonStrategyBuilder.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCompetitiveTeamPokemonFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ability",
                table: "TeamPokemon",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AttackEv",
                table: "TeamPokemon",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DefenseEv",
                table: "TeamPokemon",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "TeamPokemon",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HpEv",
                table: "TeamPokemon",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsShiny",
                table: "TeamPokemon",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Item",
                table: "TeamPokemon",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "TeamPokemon",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                table: "TeamPokemon",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SpecialAttackEv",
                table: "TeamPokemon",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpecialDefenseEv",
                table: "TeamPokemon",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpeedEv",
                table: "TeamPokemon",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeraType",
                table: "TeamPokemon",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ability",
                table: "TeamPokemon");

            migrationBuilder.DropColumn(
                name: "AttackEv",
                table: "TeamPokemon");

            migrationBuilder.DropColumn(
                name: "DefenseEv",
                table: "TeamPokemon");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "TeamPokemon");

            migrationBuilder.DropColumn(
                name: "HpEv",
                table: "TeamPokemon");

            migrationBuilder.DropColumn(
                name: "IsShiny",
                table: "TeamPokemon");

            migrationBuilder.DropColumn(
                name: "Item",
                table: "TeamPokemon");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "TeamPokemon");

            migrationBuilder.DropColumn(
                name: "Nickname",
                table: "TeamPokemon");

            migrationBuilder.DropColumn(
                name: "SpecialAttackEv",
                table: "TeamPokemon");

            migrationBuilder.DropColumn(
                name: "SpecialDefenseEv",
                table: "TeamPokemon");

            migrationBuilder.DropColumn(
                name: "SpeedEv",
                table: "TeamPokemon");

            migrationBuilder.DropColumn(
                name: "TeraType",
                table: "TeamPokemon");
        }
    }
}
