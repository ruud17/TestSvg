using System.ComponentModel.DataAnnotations.Schema;

namespace AgriBook.DB.Models
{
    public abstract class Amount
    {
        public int AmountId { get; set; }
        public decimal Quantity { get; set; }
        public MetricUnit MetricUnit { get; set; }
    }
}
