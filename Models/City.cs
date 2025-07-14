using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VerstaTestTask.Models
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string CityRegion { get; set; }

        [Required]
        [MaxLength(100)]
        public string CityName { get; set; }

    }
}
