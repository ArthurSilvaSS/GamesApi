using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamesAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddPlatformEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.CreateTable(
    name: "Platforms",
    columns: table => new
    {
        Id = table.Column<int>(type: "int", nullable: false)
            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
        Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
            .Annotation("MySql:CharSet", "utf8mb4"),
        Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
            .Annotation("MySql:CharSet", "utf8mb4"),
        PlatformType = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
            .Annotation("MySql:CharSet", "utf8mb4")
    },
    constraints: table =>
    {
        table.PrimaryKey("PK_Platforms", x => x.Id);
    })
    .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GamePlatforms",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false),
                    PlatformId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlatforms", x => new { x.GameId, x.PlatformId });
                    table.ForeignKey(
                        name: "FK_GamePlatforms_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePlatforms_Plataforms_PlataformId",
                        column: x => x.PlatformId,
                        principalTable: "Platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlatforms_PlatformId",
                table: "GamePlatforms",
                column: "PlatformId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamePlatforms");

            migrationBuilder.DropTable(
                name: "Platforms");

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "Genre", "Name", "Platform", "Publisher", "imageUrl" },
                values: new object[] { 1, "Description Cyberpunk 2077 ", "RPG", "Cyberpunk 2077", "Steam", "CD Projekt Red", "https://store-images.s-microsoft.com/image/apps.64394.13510798887568268.0b1b1b1b-0b1b-0b1b-0b1b-0b1b0b1b0b1b?w=672&h=378&q=80&mode=letterbox&background=%23FFE4E4E4&format=jpg" });
        }
    }
}
