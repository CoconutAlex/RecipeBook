using RecipeBook.Models.Domain;
using RecipeBook.Models.Domain.Requests.Ingredient;

namespace RecipeBook.Repositories.Interface
{
    public interface IIngredientRepository
    {
        #region Ingredients

        Task<IEnumerable<Ingredient>> GetAllIngredients_Repos();
        Task<Ingredient> GetIngredient_Repos(Guid id);
        Task<Ingredient> AddIngredient_Repos(AddUpdateIngredientRequest addIngredientRequest);
        Task<Ingredient> DeleteIngredient_Repos(Guid id);
        Task<Ingredient> UpdateIngredient_Repos(Guid id, AddUpdateIngredientRequest updateIngredientRequest);

        #endregion

        #region Ingredients Names

        Task<IEnumerable<IngredientName>> GetAllIngredientsNames_Repos();
        Task<IngredientName> GetIngredientName_Repos(Guid id);
        Task<IngredientName> AddIngredientName_Repos(IngredientName ingredientName);
        Task<IngredientName> DeleteIngredientName_Repos(Guid id);
        Task<IngredientName> UpdateIngredientName_Repos(Guid id, IngredientName ingredientName);

        #endregion

        #region Ingredients Quantities

        Task<IEnumerable<IngredientQuantity>> GetAllIngredientsQuantities_Repos();
        Task<IngredientQuantity> GetIngredientQuantity_Repos(Guid id);
        Task<IngredientQuantity> AddIngredientQuantity_Repos(IngredientQuantity ingredientQuantity);
        Task<IngredientQuantity> DeleteIngredientQuantity_Repos(Guid id);
        Task<IngredientQuantity> UpdateIngredientQuantity_Repos(Guid id, IngredientQuantity ingredientQuantity);

        #endregion
    }
}
