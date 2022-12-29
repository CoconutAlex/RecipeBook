using RecipeBook.Models.Domain;
using RecipeBook.Models.Domain.Requests.Recipe;

namespace RecipeBook.Repositories.Interface
{
    public interface IRecipeRepository
    {
        #region Recipes

        Task<IEnumerable<Recipe>> GetAllRecipes_Repos();
        Task<Recipe> GetRecipe_Repos(Guid id);
        Task<Recipe> AddRecipe_Repos(AddUpdateRecipeRequest addRecipeRequest);
        Task<Recipe> DeleteRecipe_Repos(Guid id);
        Task<Recipe> UpdateRecipe_Repos(Guid id, AddUpdateRecipeRequest updateRecipeRequest);

        #endregion
    }
}
