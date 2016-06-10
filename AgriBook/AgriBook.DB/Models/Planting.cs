using System;
using System.Collections.Generic;

namespace AgriBook.DB.Models
{
    public class Planting
    {
        public int Id { get; set; }
        public DateTime Season { get; set; }

        public virtual Parcel Parcel { get; set; }

        public virtual ICollection<PlantingCrop> PlantingCrops { get; set; }

        public virtual ICollection<PlantingFertilizer> PlantingFertilizers { get; set; }

        public virtual ICollection<Yield> Yields { get; set; }
    }
}
