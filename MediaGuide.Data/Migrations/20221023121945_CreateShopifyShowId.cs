using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaGuide.Data.Migrations
{
    public partial class CreateShopifyShowId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpotifyApiKey",
                table: "Shows");

            migrationBuilder.AddColumn<string>(
                name: "SpotifyShowId",
                table: "Shows",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpotifyShowId",
                table: "Shows");

            migrationBuilder.AddColumn<string>(
                name: "SpotifyApiKey",
                table: "Shows",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
