using System.Text.Json.Serialization;

namespace RecipeBook.Models.Dto
{
    public class Ingredient
    {
        public Guid Id { get; set; }
        public IngredientName IngredientName { get; set; }
        public IngredientQuantity IngredientQuantity { get; set; }

        //Navigation Property
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public IEnumerable<Recipe> Recipes { get; set; }
    }
}
