using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace AgriBook.DB.Models
{
    public class MetricUnit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ForeignKey("Amounts")]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 1 - weight
        /// 2 - area
        /// </summary>
        public int Type { get; set; }

        public virtual ICollection<Amount> Amounts { get; set; }
    }
}
