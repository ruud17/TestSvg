using System.ComponentModel.DataAnnotations.Schema;

namespace AgriBook.DB.Models
{
    public class PlantingFertilizer : Amount
    {
        public virtual Planting Planting { get; set; }
        public virtual Fertilizer Fertilizer { get; set; }
    }
}
