using Microsoft.EntityFrameworkCore;
using RecipeBook.Models.Domain;

namespace RecipeBook.Data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        public DbSet<Recipe> Recipies { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
    }
}
