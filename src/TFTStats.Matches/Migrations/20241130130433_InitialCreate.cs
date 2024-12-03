using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFTStats.Matches.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    GameVersion = table.Column<string>(type: "text", nullable: true),
                    GameType = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Participant",
                columns: table => new
                {
                    MatchId = table.Column<string>(type: "text", nullable: false),
                    Puuid = table.Column<string>(type: "text", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: true),
                    Placement = table.Column<int>(type: "integer", nullable: true),
                    Win = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participant", x => new { x.Puuid, x.MatchId });
                    table.ForeignKey(
                        name: "FK_Participant_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trait",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NumberOfUnits = table.Column<int>(type: "integer", nullable: true),
                    CurrentTier = table.Column<int>(type: "integer", nullable: true),
                    TotalTier = table.Column<int>(type: "integer", nullable: true),
                    ParticipantId = table.Column<string>(type: "text", nullable: true),
                    MatchId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trait", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trait_Participant_ParticipantId_MatchId",
                        columns: x => new { x.ParticipantId, x.MatchId },
                        principalTable: "Participant",
                        principalColumns: new[] { "Puuid", "MatchId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Tier = table.Column<int>(type: "integer", nullable: true),
                    ItemNames = table.Column<string[]>(type: "text[]", nullable: false),
                    ParticipantId = table.Column<string>(type: "text", nullable: true),
                    MatchId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Unit_Participant_ParticipantId_MatchId",
                        columns: x => new { x.ParticipantId, x.MatchId },
                        principalTable: "Participant",
                        principalColumns: new[] { "Puuid", "MatchId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participant_MatchId",
                table: "Participant",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Trait_ParticipantId_MatchId",
                table: "Trait",
                columns: new[] { "ParticipantId", "MatchId" });

            migrationBuilder.CreateIndex(
                name: "IX_Unit_ParticipantId_MatchId",
                table: "Unit",
                columns: new[] { "ParticipantId", "MatchId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trait");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropTable(
                name: "Participant");

            migrationBuilder.DropTable(
                name: "Matches");
        }
    }
}
