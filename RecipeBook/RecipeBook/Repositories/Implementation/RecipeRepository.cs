using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RecipeBook.Data;
using RecipeBook.Models.Domain;
using RecipeBook.Repositories.Interface;

namespace RecipeBook.Repositories.Implementation
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly DataBaseContext dataBaseContext;

        public RecipeRepository(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipies_Repos()
        {
            return await dataBaseContext.Recipies.ToListAsync();
        }

        public async Task<Recipe> GetRecipe_Repos(Guid id)
        {
            return await dataBaseContext.Recipies.FirstOrDefaultAsync(recipe => recipe.Id == id);
        }

        public async Task<Recipe> AddRecipe_Repos(Recipe recipe)
        {
            recipe.Id = Guid.NewGuid();

            await dataBaseContext.Recipies.AddAsync(recipe);
            await dataBaseContext.SaveChangesAsync();

            return recipe;
        }

        public async Task<Recipe> DeleteRecipe_Repos(Guid id)
        {
            var deletedRecipe = await GetRecipe_Repos(id);

            if (deletedRecipe == null) return null;

            dataBaseContext.Recipies.Remove(deletedRecipe);
            await dataBaseContext.SaveChangesAsync();

            return deletedRecipe;
        }

        public async Task<Recipe> UpdateRecipe_Repos(Guid id, Recipe recipe)
        {
            var updatedRecipe = await GetRecipe_Repos(id);

            if (updatedRecipe == null) return null;

            updatedRecipe.Title = recipe.Title;
            updatedRecipe.Description = recipe.Description;
            updatedRecipe.Steps = recipe.Steps;
            updatedRecipe.Portions = recipe.Portions;
            updatedRecipe.Duration = recipe.Duration;
            updatedRecipe.Difficulty = recipe.Difficulty;

            await dataBaseContext.SaveChangesAsync();

            return updatedRecipe;

        }
    }
}
