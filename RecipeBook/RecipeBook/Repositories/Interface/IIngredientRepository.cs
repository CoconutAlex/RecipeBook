using RecipeBook.Models.Domain;

namespace RecipeBook.Repositories.Interface
{
    public interface IIngredientRepository
    {
        Task<IEnumerable<Ingredient>> GetAllIngredients_Repos();
        Task<Ingredient> GetIngredient_Repos(Guid id);
        Task<Ingredient> AddIngredient_Repos(Ingredient ingredient);
        Task<Ingredient> DeleteIngredient_Repos(Guid id);
        Task<Ingredient> UpdateIngredient_Repos(Guid id, Ingredient ingredient);
    }
}
