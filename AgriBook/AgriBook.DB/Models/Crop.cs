using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriBook.DB.Models
{
    public class Crop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ForeignKey("PlantingCrops")]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Color { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string ImageName { get; set; }

        public virtual ICollection<Amount> PlantingCrops { get; set; }
    }
}
