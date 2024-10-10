using System.ComponentModel.DataAnnotations;

namespace bad4.Models
{
    // Represents an allergen that can be associated with a stock item.
    public class Allergen
    {
        // Unique identifier for the allergen.
        [Key]
        public int AllergenID { get; set; }

        // Name of the allergen.
        public string Name { get; set; } = null!;

        // ID of the stock item associated with this allergen.
        public int StockID { get; set; }

        // Stock item associated with this allergen.
        public Stock Stock { get; set; } = null!;
    }
}
