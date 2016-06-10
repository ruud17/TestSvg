using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriBook.DB.Models
{
    public class Fertilizer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ForeignKey("PlantingFertilizers")]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Amount> PlantingFertilizers { get; set; }
    }
}
