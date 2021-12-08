using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.DAL.Entities
{
    public class Room
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public int RoomCategoryId { get; set; }
        [Required, ForeignKey("RoomCategoryId")]
        public RoomCategory RoomCategory { get; set; }
    }
}
