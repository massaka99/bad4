using System.ComponentModel.DataAnnotations;

namespace bad4.Models
{
    // Represents a recipe containing ingredients and optionally associated with baking goods.
    public class Recipe
    {
        // Unique identifier for the recipe.
        [Key]
        public int RecipeID { get; set; }

        // Name of the recipe.
        public string Name { get; set; }

        // Collection of ingredients required for the recipe.
        public ICollection<Ingredients> Ingredients { get; } = new List<Ingredients>();

        // Baking goods associated with the recipe.
        public BakingGoods? BakingGoods { get; set; }
    }
}
