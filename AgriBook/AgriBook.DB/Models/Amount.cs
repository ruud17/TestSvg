using System.ComponentModel.DataAnnotations.Schema;

namespace AgriBook.DB.Models
{
    public abstract class Amount
    {
        public int AmountId { get; set; }
        public decimal Quantity { get; set; }
        public virtual MetricUnit MetricUnit { get; set; }
    }
}
