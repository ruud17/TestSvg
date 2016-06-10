using System.ComponentModel.DataAnnotations.Schema;

namespace AgriBook.DB.Models
{
    public class Yield : Amount
    {
        public string Name { get; set; }
        public virtual Planting Planting { get; set; }
    }
}
