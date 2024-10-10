using System.ComponentModel.DataAnnotations;

namespace bad4.Models
{
    // Represents a stock item containing quantity, name, and optionally associated with ingredients and allergens.
    public class Stock
    {
        // Unique identifier for the stock item.
        [Key]
        public int StockID { get; set; }

        // Quantity of the stock item.
        public int Quantity { get; set; }

        // Name of the stock item.
        public string Name { get; set; }

        // Collection of ingredients associated with the stock item.
        public ICollection<Ingredients> Ingredients { get; } = new List<Ingredients>();

        // Allergen associated with the stock item.
        public Allergen? Allergen { get; set; }
    }
}
