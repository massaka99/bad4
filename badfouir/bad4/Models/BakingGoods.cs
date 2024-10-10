using Azure;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 
using System.Reflection.Metadata; 

namespace bad4.Models
{
    // Represents baking goods that are part of a company order and associated with a recipe.
    public class BakingGoods
    {
        // Unique identifier for the baking goods.
        [Key]
        public int BakingGoodsID { get; set; }

        // Name of the baking goods.
        public string Name { get; set; }

        // Quantity of the baking goods.
        public int Quantity { get; set; }

        // Company order associated with the baking goods.
        public CompanyOrders CompanyOrders { get; set; } = null!;

        // ID of the company order associated with the baking goods.
        public int CompanyOrdersID { get; set; }

        // Recipe associated with the baking goods.
        public Recipe Recipe { get; set; } = null!;

        // ID of the recipe associated with the baking goods.
        public int RecipeID { get; set; }

        // Collection of batches associated with the baking goods.
        public ICollection<Batch> Batch { get; } = new List<Batch>(); 

    }
}
