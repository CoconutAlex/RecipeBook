namespace RecipeBook.Models.Domain
{
    public class Ingredient
    {
        public Guid Id { get; set; }
        public IngredientName IngredientName { get; set; }
        public IngredientQuantity IngredientQuantity { get; set; }

        //Navigation Property
        public IEnumerable<Recipe> Recipies { get; set; }
    }
}
