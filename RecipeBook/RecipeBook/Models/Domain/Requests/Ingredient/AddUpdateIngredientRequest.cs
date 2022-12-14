namespace RecipeBook.Models.Domain.Requests.Ingredient
{
    public class AddUpdateIngredientRequest
    {
        public Guid IngredientQuantityId { get; set; }
        public Guid IngredientNameId { get; set; }
    }
}
