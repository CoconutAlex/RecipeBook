using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RecipeBook.Models.Domain
{
    public class Ingredient
    {
        public Guid Id { get; set; }
        public string Quantity { get; set; }
        public string Name { get; set; }

        //Navigation Property
        public IEnumerable<Recipe> Recipies { get; set; }
    }
}
