using Microsoft.EntityFrameworkCore;
using RecipeBook.Data;
using RecipeBook.Models.Domain;
using RecipeBook.Models.Domain.Requests.Ingredient;
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

        #region Ingredients

        public async Task<IEnumerable<Ingredient>> GetAllIngredients_Repos()
        {
            try
            {
                return await dataBaseContext.Ingredients
                    .Include(x => x.Recipes)
                    .Include(x => x.IngredientName)
                    .Include(x => x.IngredientQuantity)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return null;
            }
        }

        public async Task<Ingredient> GetIngredient_Repos(Guid id)
        {
            try
            {
                return await dataBaseContext.Ingredients
                    .Include(x => x.Recipes)
                    .Include(x => x.IngredientName)
                    .Include(x => x.IngredientQuantity)
                    .FirstOrDefaultAsync(ingredient => ingredient.Id == id);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return null;
            }
        }

        public async Task<Ingredient> AddIngredient_Repos(AddUpdateIngredientRequest addIngredientRequest)
        {
            try
            {
                var ingredient = new Ingredient()
                {
                    Id = Guid.NewGuid(),
                    IngredientName = await GetIngredientName_Repos(addIngredientRequest.IngredientNameId),
                    IngredientQuantity = await GetIngredientQuantity_Repos(addIngredientRequest.IngredientQuantityId)
                };

                await dataBaseContext.Ingredients.AddAsync(ingredient);
                await dataBaseContext.SaveChangesAsync();

                return ingredient;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return null;
            }
        }

        public async Task<Ingredient> DeleteIngredient_Repos(Guid id)
        {
            try
            {
                var deletedIngredient = await GetIngredient_Repos(id);

                if (deletedIngredient == null) return null;

                dataBaseContext.Ingredients.Remove(deletedIngredient);
                await dataBaseContext.SaveChangesAsync();

                return deletedIngredient;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return null;
            }
        }

        public async Task<Ingredient> UpdateIngredient_Repos(Guid id, AddUpdateIngredientRequest updateIngredientRequest)
        {
            try
            {
                var updatedIngredient = await GetIngredient_Repos(id);

                if (updatedIngredient == null) return null;

                updatedIngredient.IngredientName = await GetIngredientName_Repos(updateIngredientRequest.IngredientNameId);
                updatedIngredient.IngredientQuantity = await GetIngredientQuantity_Repos(updateIngredientRequest.IngredientQuantityId);

                await dataBaseContext.SaveChangesAsync();

                return updatedIngredient;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return null;
            }
        }

        #endregion

        #region Ingredients Names

        public async Task<IEnumerable<IngredientName>> GetAllIngredientsNames_Repos()
        {
            try
            {
                return await dataBaseContext.IngredientsNames
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return null;
            }
        }

        public async Task<IngredientName> GetIngredientName_Repos(Guid id)
        {
            try
            {
                return await dataBaseContext.IngredientsNames
                .FirstOrDefaultAsync(ingredientName => ingredientName.Id == id);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return null;
            }
        }

        public async Task<IngredientName> AddIngredientName_Repos(IngredientName ingredientName)
        {
            try
            {
                ingredientName.Id = Guid.NewGuid();

                await dataBaseContext.IngredientsNames.AddAsync(ingredientName);
                await dataBaseContext.SaveChangesAsync();

                return ingredientName;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return null;
            }
        }

        public async Task<IngredientName> DeleteIngredientName_Repos(Guid id)
        {
            try
            {
                var deletedIngredientName = await GetIngredientName_Repos(id);

                if (deletedIngredientName == null) return null;

                dataBaseContext.IngredientsNames.Remove(deletedIngredientName);
                await dataBaseContext.SaveChangesAsync();

                return deletedIngredientName;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return null;
            }
        }

        public async Task<IngredientName> UpdateIngredientName_Repos(Guid id, IngredientName ingredientName)
        {
            try
            {
                var updatedIngredientName = await GetIngredientName_Repos(id);

                if (updatedIngredientName == null) return null;

                updatedIngredientName.Name = ingredientName.Name;

                await dataBaseContext.SaveChangesAsync();

                return updatedIngredientName;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return null;
            }
        }

        #endregion

        #region Ingredients Quantities

        public async Task<IEnumerable<IngredientQuantity>> GetAllIngredientsQuantities_Repos()
        {
            try
            {
                return await dataBaseContext.IngredientsQuantities
                .ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return null;
            }
        }

        public async Task<IngredientQuantity> GetIngredientQuantity_Repos(Guid id)
        {
            try
            {
                return await dataBaseContext.IngredientsQuantities
                .FirstOrDefaultAsync(ingredientQuantity => ingredientQuantity.Id == id);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return null;
            }
        }

        public async Task<IngredientQuantity> AddIngredientQuantity_Repos(IngredientQuantity ingredientQuantity)
        {
            try
            {
                ingredientQuantity.Id = Guid.NewGuid();

                await dataBaseContext.IngredientsQuantities.AddAsync(ingredientQuantity);
                await dataBaseContext.SaveChangesAsync();

                return ingredientQuantity;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return null;
            }
        }

        public async Task<IngredientQuantity> DeleteIngredientQuantity_Repos(Guid id)
        {
            try
            {
                var deletedIngredientQuantity = await GetIngredientQuantity_Repos(id);

                if (deletedIngredientQuantity == null) return null;

                dataBaseContext.IngredientsQuantities.Remove(deletedIngredientQuantity);
                await dataBaseContext.SaveChangesAsync();

                return deletedIngredientQuantity;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return null;
            }
        }

        public async Task<IngredientQuantity> UpdateIngredientQuantity_Repos(Guid id, IngredientQuantity ingredientQuantity)
        {
            try
            {
                var updatedIngredientQuantity = await GetIngredientQuantity_Repos(id);

                if (updatedIngredientQuantity == null) return null;

                updatedIngredientQuantity.Quantity = ingredientQuantity.Quantity;

                await dataBaseContext.SaveChangesAsync();

                return updatedIngredientQuantity;
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
