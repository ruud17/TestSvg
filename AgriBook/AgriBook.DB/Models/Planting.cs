using System;
using System.Collections.Generic;

namespace AgriBook.DB.Models
{
    public class Planting
    {
        public int Id { get; set; }
        public DateTime Season { get; set; }

        public Parcel Parcel { get; set; }

        public ICollection<PlantingCrop> PlantingCrops { get; set; }

        public ICollection<PlantingFertilizer> PlantingFertilizers { get; set; }

        public ICollection<Yield> Yields { get; set; }
    }
}
