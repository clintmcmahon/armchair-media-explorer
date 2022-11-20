using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaGuide.Data.Migrations
{
    public partial class CreateDisplayField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Display",
                table: "Episodes",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Display",
                table: "Episodes");
        }
    }
}
