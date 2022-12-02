namespace RecipeBook.Models.Dto.Requests.Recipe
{
    public class AddIngredientRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Steps { get; set; }
        public int Portions { get; set; }
        public int Duration { get; set; }
        public Enums.DomainEnums.Difficulty Difficulty { get; set; }
    }
}
