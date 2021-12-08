using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.DAL.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
        [Required]
        public OrderType Type { get; set; }
        public string UserLogin { get; set; }
        [ForeignKey("UserLogin")]
        public User User { get; set; }
        public int RoomId { get; set; }
        [Required, ForeignKey("RoomId")]
        public Room Room { get; set; }
    }

    public enum OrderType
    {
        Booked,
        Paid
    }
}
