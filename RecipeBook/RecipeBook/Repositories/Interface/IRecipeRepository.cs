using RecipeBook.Models.Domain;

namespace RecipeBook.Repositories.Interface
{
    public interface IRecipeRepository
    {
        Task<IEnumerable<Recipe>> GetAllRecipies_Repos();
        Task<Recipe> GetRecipe_Repos(Guid id);
        Task<Recipe> AddRecipe_Repos(Recipe recipe);
        Task<Recipe> DeleteRecipe_Repos(Guid id);
        Task<Recipe> UpdateRecipe_Repos(Guid id, Recipe recipe);
    }
}
