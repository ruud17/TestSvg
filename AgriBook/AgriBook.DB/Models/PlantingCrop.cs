using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriBook.DB.Models
{
    public class PlantingCrop : Amount
    {
        public Planting Planting { get; set; }
        public Crop Crop { get; set; }
    }
}
