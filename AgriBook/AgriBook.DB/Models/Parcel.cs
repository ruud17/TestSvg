using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriBook.DB.Models
{
    public class Parcel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ForeignKey("Plantings")]
        public int Id { get; set; }
        [Required]
        public string GruntId { get; set; }
        [MaxLength(450)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public string OwnerName { get; set; }
        [Required]
        public string Points { get; set; }

        public ICollection<ParcelArea> ParcelAreas { get; set; }

        public ICollection<Planting> Plantings { get; set; }
    }
}
