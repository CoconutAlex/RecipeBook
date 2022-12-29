using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Serverapp.Migrations
{
    public partial class AddRecepeImageMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Recipes");
        }
    }
}
