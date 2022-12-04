using Microsoft.EntityFrameworkCore;
using RecipeBook.Data;
using RecipeBook.Models.Domain;
using RecipeBook.Repositories.Interface;

namespace RecipeBook.Repositories.Implementation
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly DataBaseContext dataBaseContext;

        public IngredientRepository(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        public async Task<IEnumerable<Ingredient>> GetAllIngredients_Repos()
        {
            return await dataBaseContext.Ingredients
                .Include(x => x.Recipies)
                .ToListAsync();
        }

        public async Task<Ingredient> GetIngredient_Repos(Guid id)
        {
            return await dataBaseContext.Ingredients
                .Include(x => x.Recipies)
                .FirstOrDefaultAsync(ingredient => ingredient.Id == id);
        }

        public async Task<Ingredient> AddIngredient_Repos(Ingredient ingredient)
        {
            ingredient.Id = Guid.NewGuid();

            await dataBaseContext.Ingredients.AddAsync(ingredient);
            await dataBaseContext.SaveChangesAsync();

            return ingredient;
        }

        public async Task<Ingredient> DeleteIngredient_Repos(Guid id)
        {
            var deletedIngredient = await GetIngredient_Repos(id);

            if (deletedIngredient == null) return null;

            dataBaseContext.Ingredients.Remove(deletedIngredient);
            await dataBaseContext.SaveChangesAsync();

            return deletedIngredient;
        }

        public async Task<Ingredient> UpdateIngredient_Repos(Guid id, Ingredient ingredient)
        {
            var updatedIngredient = await GetIngredient_Repos(id);

            if (updatedIngredient == null) return null;

            updatedIngredient.Quantity = ingredient.Quantity;
            updatedIngredient.Name = ingredient.Name;

            await dataBaseContext.SaveChangesAsync();

            return updatedIngredient;
        }
    }
}
