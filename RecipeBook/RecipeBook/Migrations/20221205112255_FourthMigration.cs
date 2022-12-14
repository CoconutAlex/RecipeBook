using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeBook.Migrations
{
    public partial class FourthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Ingredients");

            migrationBuilder.AddColumn<Guid>(
                name: "IngredientNameId",
                table: "Ingredients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IngredientQuantityId",
                table: "Ingredients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IngredientsNames",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientsNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngredientsQuantities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientsQuantities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_IngredientNameId",
                table: "Ingredients",
                column: "IngredientNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_IngredientQuantityId",
                table: "Ingredients",
                column: "IngredientQuantityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_IngredientsNames_IngredientNameId",
                table: "Ingredients",
                column: "IngredientNameId",
                principalTable: "IngredientsNames",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_IngredientsQuantities_IngredientQuantityId",
                table: "Ingredients",
                column: "IngredientQuantityId",
                principalTable: "IngredientsQuantities",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_IngredientsNames_IngredientNameId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_IngredientsQuantities_IngredientQuantityId",
                table: "Ingredients");

            migrationBuilder.DropTable(
                name: "IngredientsNames");

            migrationBuilder.DropTable(
                name: "IngredientsQuantities");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_IngredientNameId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_IngredientQuantityId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "IngredientNameId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "IngredientQuantityId",
                table: "Ingredients");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Quantity",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
