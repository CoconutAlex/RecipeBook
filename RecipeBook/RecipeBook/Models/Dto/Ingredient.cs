using System.Text.Json.Serialization;

namespace RecipeBook.Models.Dto
{
    public class Ingredient
    {
        public Guid Id { get; set; }
        public string Quantity { get; set; }
        public string Name { get; set; }

        //Navigation Property
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<Recipe> Recipies { get; set; }
    }
}
