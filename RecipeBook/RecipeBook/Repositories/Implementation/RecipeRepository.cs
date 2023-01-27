using Microsoft.EntityFrameworkCore;
using RecipeBook.Data;
using RecipeBook.Models.Domain;
using RecipeBook.Models.Domain.Requests.Recipe;
using RecipeBook.Repositories.Interface;
using System.Runtime.CompilerServices;

namespace RecipeBook.Repositories.Implementation
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly DataBaseContext dataBaseContext;

        #region Recipes

        public RecipeRepository(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipes_Repos()
        {
            return await dataBaseContext.Recipes
                .Include(x => x.Ingredients).ThenInclude(y => y.IngredientName)
                .Include(x => x.Ingredients).ThenInclude(y => y.IngredientQuantity)
                .ToListAsync();
        }

        public async Task<Recipe> GetRecipe_Repos(Guid id)
        {
            return await dataBaseContext.Recipes
                .Include(x => x.Ingredients).ThenInclude(y => y.IngredientName).AsNoTracking()
                .Include(x => x.Ingredients).ThenInclude(y => y.IngredientQuantity).AsNoTracking()
                .FirstOrDefaultAsync(recipe => recipe.Id == id);
        }

        public async Task<Recipe> AddRecipe_Repos(AddUpdateRecipeRequest addRecipeRequest)
        {
            var tempIngredients = new List<Ingredient>();
            foreach (var item in addRecipeRequest.Ingredients)
            {
                var newIngredient = new Ingredient()
                {
                    Id = item.IngredientId,
                    IngredientName = dataBaseContext.IngredientsNames.FirstOrDefault(ingredientName => ingredientName.Id == dataBaseContext.Ingredients.FirstOrDefault(ingredient => ingredient.Id == item.IngredientId).IngredientName.Id),
                    IngredientQuantity = dataBaseContext.IngredientsQuantities.FirstOrDefault(ingredientQuantity => ingredientQuantity.Id == dataBaseContext.Ingredients.FirstOrDefault(ingredient => ingredient.Id == item.IngredientId).IngredientQuantity.Id)
                };
            }

            var insertedRecipe = new Recipe()
            {
                Id = Guid.NewGuid(),
                Title = addRecipeRequest.Title,
                Description = addRecipeRequest.Description,
                Difficulty = addRecipeRequest.Difficulty,
                Duration = addRecipeRequest.Duration,
                Portions = addRecipeRequest.Portions,
                Steps = addRecipeRequest.Steps,
                ImageName = addRecipeRequest.ImageName,
                Ingredients = tempIngredients
            };

            await dataBaseContext.Recipes.AddAsync(insertedRecipe);
            await dataBaseContext.SaveChangesAsync();

            return insertedRecipe;
        }

        public async Task<Recipe> DeleteRecipe_Repos(Guid id)
        {
            var deletedRecipe = await GetRecipe_Repos(id);

            if (deletedRecipe == null) return null;

            dataBaseContext.Recipes.Remove(deletedRecipe);
            await dataBaseContext.SaveChangesAsync();

            return deletedRecipe;
        }

        public async Task<Recipe> UpdateRecipe_Repos(Guid id, AddUpdateRecipeRequest updateRecipeRequest)
        {
            var updatedRecipe = await GetRecipe_Repos(id);

            if (updatedRecipe == null) return null;

            updatedRecipe.Title = updateRecipeRequest.Title;
            updatedRecipe.Description = updateRecipeRequest.Description;
            updatedRecipe.Steps = updateRecipeRequest.Steps;
            updatedRecipe.Portions = updateRecipeRequest.Portions;
            updatedRecipe.Duration = updateRecipeRequest.Duration;
            updatedRecipe.Difficulty = updateRecipeRequest.Difficulty;
            updatedRecipe.ImageName = updateRecipeRequest.ImageName;
            updatedRecipe.Ingredients = updateRecipeRequest.Ingredients.Select(
                        q => new Ingredient
                        {
                            Id = q.IngredientId,
                            IngredientName = dataBaseContext.IngredientsNames.FirstOrDefault(ingredientName => ingredientName.Id == dataBaseContext.Ingredients.FirstOrDefault(ingredient => ingredient.Id == q.IngredientId).IngredientName.Id),
                            IngredientQuantity = dataBaseContext.IngredientsQuantities.FirstOrDefault(ingredientQuantity => ingredientQuantity.Id == dataBaseContext.Ingredients.FirstOrDefault(ingredient => ingredient.Id == q.IngredientId).IngredientQuantity.Id)
                        }).ToList();

            dataBaseContext.Recipes.Update(updatedRecipe);
            await dataBaseContext.SaveChangesAsync();

            return updatedRecipe;

        }

        #endregion
    }
}
