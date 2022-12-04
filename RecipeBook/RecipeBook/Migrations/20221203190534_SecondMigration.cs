using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeBook.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientRecipe_Recipies_RecipeId",
                table: "IngredientRecipe");

            migrationBuilder.RenameColumn(
                name: "RecipeId",
                table: "IngredientRecipe",
                newName: "RecipiesId");

            migrationBuilder.RenameIndex(
                name: "IX_IngredientRecipe_RecipeId",
                table: "IngredientRecipe",
                newName: "IX_IngredientRecipe_RecipiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientRecipe_Recipies_RecipiesId",
                table: "IngredientRecipe",
                column: "RecipiesId",
                principalTable: "Recipies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientRecipe_Recipies_RecipiesId",
                table: "IngredientRecipe");

            migrationBuilder.RenameColumn(
                name: "RecipiesId",
                table: "IngredientRecipe",
                newName: "RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_IngredientRecipe_RecipiesId",
                table: "IngredientRecipe",
                newName: "IX_IngredientRecipe_RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientRecipe_Recipies_RecipeId",
                table: "IngredientRecipe",
                column: "RecipeId",
                principalTable: "Recipies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
