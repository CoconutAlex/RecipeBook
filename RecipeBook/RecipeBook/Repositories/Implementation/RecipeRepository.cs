using Microsoft.EntityFrameworkCore;
using RecipeBook.Data;
using RecipeBook.Models.Domain;
using RecipeBook.Models.Domain.Requests.Recipe;
using RecipeBook.Repositories.Interface;

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
            try
            {
                return await dataBaseContext.Recipes
                    .Include(x => x.Ingredients).ThenInclude(y => y.IngredientName)
                    .Include(x => x.Ingredients).ThenInclude(y => y.IngredientQuantity)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return null;
            }
        }

        public async Task<Recipe> GetRecipe_Repos(Guid id)
        {
            try
            {
                return await dataBaseContext.Recipes
                    .Include(x => x.Ingredients).ThenInclude(y => y.IngredientName).AsNoTracking()
                    .Include(x => x.Ingredients).ThenInclude(y => y.IngredientQuantity).AsNoTracking()
                    .FirstOrDefaultAsync(recipe => recipe.Id == id);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return null;
            }
        }

        public async Task<Recipe> AddRecipe_Repos(AddUpdateRecipeRequest addRecipeRequest)
        {
            try
            {
                var insertedIngredients = new List<Ingredient>();
                foreach (var item in addRecipeRequest.Ingredients)
                {
                    var foundIngredient = dataBaseContext.Ingredients.AsNoTracking().FirstOrDefault(currentIngredient => currentIngredient.IngredientQuantity.Id == item.IngredientQuantityId && currentIngredient.IngredientName.Id == item.IngredientNameId);
                    var newIngredient = new Ingredient()
                    {
                        Id = foundIngredient != null ? foundIngredient.Id : Guid.NewGuid(),
                        IngredientName = dataBaseContext.IngredientsNames.FirstOrDefault(ingredientName => ingredientName.Id == item.IngredientNameId),
                        IngredientQuantity = dataBaseContext.IngredientsQuantities.FirstOrDefault(ingredientQuantity => ingredientQuantity.Id == item.IngredientQuantityId),
                    };

                    if (foundIngredient != null)
                    {
                        dataBaseContext.Entry(newIngredient).State = EntityState.Unchanged;
                    }

                    insertedIngredients.Add(newIngredient);
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
                    Ingredients = insertedIngredients
                };

                await dataBaseContext.Recipes.AddAsync(insertedRecipe);
                await dataBaseContext.SaveChangesAsync();

                return insertedRecipe;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return null;
            }
        }

        public async Task<Recipe> DeleteRecipe_Repos(Guid id)
        {
            try
            {
                var deletedRecipe = await GetRecipe_Repos(id);

                if (deletedRecipe == null) return null;

                dataBaseContext.Recipes.Remove(deletedRecipe);
                await dataBaseContext.SaveChangesAsync();

                return deletedRecipe;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return null;
            }
        }

        public async Task<Recipe> UpdateRecipe_Repos(Guid id, AddUpdateRecipeRequest updateRecipeRequest)
        {
            try
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

                var updatedIngredients = new List<Ingredient>();
                foreach (var item in updateRecipeRequest.Ingredients)
                {
                    var foundIngredient = dataBaseContext.Ingredients.AsNoTracking().FirstOrDefault(currentIngredient => currentIngredient.IngredientQuantity.Id == item.IngredientQuantityId && currentIngredient.IngredientName.Id == item.IngredientNameId);
                    var newIngredient = new Ingredient()
                    {
                        Id = foundIngredient != null ? foundIngredient.Id : Guid.NewGuid(),
                        IngredientName = dataBaseContext.IngredientsNames.FirstOrDefault(ingredientName => ingredientName.Id == item.IngredientNameId),
                        IngredientQuantity = dataBaseContext.IngredientsQuantities.FirstOrDefault(ingredientQuantity => ingredientQuantity.Id == item.IngredientQuantityId),
                    };

                    if (foundIngredient != null)
                    {
                        var existIngredient = updatedRecipe.Ingredients.FirstOrDefault(ingredient => ingredient.Id == foundIngredient.Id);
                        if (existIngredient == null)
                        {
                            dataBaseContext.Entry(newIngredient).State = EntityState.Unchanged;
                            updatedIngredients.Add(newIngredient);
                        }
                    }
                    else
                    {
                        await dataBaseContext.Ingredients.AddAsync(newIngredient);
                        await dataBaseContext.SaveChangesAsync();

                        dataBaseContext.Entry(newIngredient).State = EntityState.Unchanged;
                        updatedIngredients.Add(newIngredient);
                    }
                }

                updatedRecipe.Ingredients = updatedIngredients;

                dataBaseContext.Recipes.Update(updatedRecipe);
                await dataBaseContext.SaveChangesAsync();

                return updatedRecipe;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return null;
            }

        }

        #endregion
    }
}
