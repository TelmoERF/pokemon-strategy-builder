using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonStrategyBuilder.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTeams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamPokemon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeamId = table.Column<int>(type: "INTEGER", nullable: false),
                    PokemonId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPokemon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamPokemon_Pokemon_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamPokemon_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamPokemon_PokemonId",
                table: "TeamPokemon",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPokemon_TeamId",
                table: "TeamPokemon",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamPokemon");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
