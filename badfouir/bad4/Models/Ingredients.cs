using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bad4.Models
{
    // Represents ingredients required for a recipe and associated with a stock item.
    public class Ingredients
    {
        // Quantity of the ingredient.
        public int Quantity { get; set; }

        // ID of the recipe associated with the ingredient.
        public int RecipeID { get; set; }

        // Recipe associated with the ingredient.
        public Recipe Recipe { get; set; } = null!;

        // ID of the stock item associated with the ingredient.
        public int StockID { get; set; }

        // Stock item associated with the ingredient.
        public Stock Stock { get; set; } = null!;
    }
}
