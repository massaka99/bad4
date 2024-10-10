using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace bad4.Models
{
    // Represents orders placed by a company.
    public class CompanyOrders
    {
        // Unique identifier for the company orders.
        [Key]
        public int CompanyOrdersID { get; set; }

        // Date of delivery for the orders.
        public string DeliveryDate { get; set; }

        // Place of delivery for the orders.
        public string DeliveryPlace { get; set; }

        // Collection of dispatches associated with the company orders.
        public ICollection<Dispatch> Dispatch { get; } = new List<Dispatch>();

        // Collection of baking goods associated with the company orders.
        public ICollection<BakingGoods> BakingGoods { get; } = new List<BakingGoods>();
    }
}
