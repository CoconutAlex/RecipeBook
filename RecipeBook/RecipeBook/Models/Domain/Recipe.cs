namespace RecipeBook.Models.Domain
{
    public class Recipe
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Steps { get; set; }
        public int Portions { get; set; }
        public int Duration { get; set; }
        public Enums.DomainEnums.Difficulty Difficulty { get; set; }

        //Navigation Property
        public IEnumerable<Ingredient> Ingredients { get; set; }
    }
}
