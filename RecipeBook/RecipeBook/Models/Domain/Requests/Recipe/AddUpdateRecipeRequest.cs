namespace RecipeBook.Models.Domain.Requests.Recipe
{
    public class AddUpdateRecipeRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Steps { get; set; }
        public int Portions { get; set; }
        public int Duration { get; set; }
        public Enums.DomainEnums.Difficulty Difficulty { get; set; }
        public IEnumerable<Models.Domain.Requests.Recipe.AddIngredientForRecipeRequest> Ingredients { get; set; }
    }
}
