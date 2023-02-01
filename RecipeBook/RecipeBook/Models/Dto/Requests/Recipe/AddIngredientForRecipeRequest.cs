namespace RecipeBook.Models.Dto.Requests.Recipe
{
    public class AddIngredientForRecipeRequest
    {
        public Guid IngredientQuantityId { get; set; }
        public Guid IngredientNameId { get; set; }
    }
}
