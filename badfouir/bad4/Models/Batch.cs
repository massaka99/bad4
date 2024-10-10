using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace bad4.Models
{
    // Represents a batch of baking goods.
    public class Batch
    {
        // Unique identifier for the batch.
        [Key]
        public int BatchID { get; set; }

        // Delay in processing the batch.
        public float Delay { get; set; }

        // Start time of the batch.
        public DateTime StartTime { get; set; }

        // Finish time of the batch.
        public DateTime FinishTime { get; set; }

        // ID of the baking goods associated with the batch.
        public int BakingGoodsID { get; set; }

        // Baking goods associated with the batch.
        public BakingGoods BakingGoods { get; set; } = null!;
    }
}
