using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriBook.DB.Models
{
    public class PlantingCrop : Amount
    {
        public virtual Planting Planting { get; set; }
        public virtual Crop Crop { get; set; }
    }
}
