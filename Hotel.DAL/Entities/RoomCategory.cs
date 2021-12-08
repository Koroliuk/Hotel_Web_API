using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.DAL.Entities
{
    public class RoomCategory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public decimal PricePerDay { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required, MaxLength(300)]
        public string Description { get; set; }
    }
}
