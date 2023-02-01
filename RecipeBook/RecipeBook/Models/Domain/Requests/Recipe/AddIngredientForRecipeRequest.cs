namespace RecipeBook.Models.Domain.Requests.Recipe
{
    public class AddIngredientForRecipeRequest
    {
        public Guid IngredientQuantityId { get; set; }
        public Guid IngredientNameId { get; set; }
    }
}
