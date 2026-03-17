using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonStrategyBuilder.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTeamPokemonMoves : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Moves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Category = table.Column<int>(type: "INTEGER", nullable: false),
                    Power = table.Column<int>(type: "INTEGER", nullable: true),
                    Accuracy = table.Column<int>(type: "INTEGER", nullable: true),
                    Pp = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moves", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamPokemonMoves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeamPokemonId = table.Column<int>(type: "INTEGER", nullable: false),
                    MoveId = table.Column<int>(type: "INTEGER", nullable: false),
                    Slot = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPokemonMoves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamPokemonMoves_Moves_MoveId",
                        column: x => x.MoveId,
                        principalTable: "Moves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamPokemonMoves_TeamPokemon_TeamPokemonId",
                        column: x => x.TeamPokemonId,
                        principalTable: "TeamPokemon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamPokemonMoves_MoveId",
                table: "TeamPokemonMoves",
                column: "MoveId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPokemonMoves_TeamPokemonId",
                table: "TeamPokemonMoves",
                column: "TeamPokemonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamPokemonMoves");

            migrationBuilder.DropTable(
                name: "Moves");
        }
    }
}
