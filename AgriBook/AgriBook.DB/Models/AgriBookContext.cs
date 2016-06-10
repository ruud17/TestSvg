using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace AgriBook.DB.Models
{
    public class AgriBookContext : DbContext
    {
        public AgriBookContext() : base("Name=DefaultConnection")
        {
            
        }
        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<Crop> Crops { get; set; }
        public DbSet<Fertilizer> Fertilizers { get; set; }
        public DbSet<Planting> Plantings { get; set; }
        public DbSet<MetricUnit> MetricUnits { get; set; }
        public DbSet<Amount> Amounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Yield>().ToTable("Yields");
            modelBuilder.Entity<PlantingCrop>().ToTable("PlantingCrops");
            modelBuilder.Entity<PlantingFertilizer>().ToTable("PlantingFertilizers");
            modelBuilder.Entity<ParcelArea>().ToTable("ParcelArea");
        }
    }
}
