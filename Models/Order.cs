using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VerstaTestTask.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public DateOnly OrderDate { get; set; }

        public long SenderCityId { get; set; }
        [ForeignKey("SenderCityId")]
        public City SenderCity {  get; set; }
        [Required]
        [MaxLength(100)]
        public string SenderAddress { get; set; }

        public long RecipientCityId { get; set; }
        [ForeignKey("RecipientCityId")]
        public City RecipientCity { get; set; }
        [Required]
        [MaxLength(100)]
        public string RecipientAddress { get; set; }

        public int CargoWeight { get; set; }
    }
}
