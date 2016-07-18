using System.ComponentModel.DataAnnotations.Schema;

namespace AgriBook.DB.Models
{
    public class PlantingFertilizer : Amount
    {
        public Planting Planting { get; set; }
        public Fertilizer Fertilizer { get; set; }
    }
}
