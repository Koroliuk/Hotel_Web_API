using Hotel.DAL.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Web_API.Models
{
    public class OrderDto
    {
        public int OrderNumber { get; set; }
        [Required(ErrorMessage = "Start date can not be empty")]
        public DateTime Start { get; set; }
        [Required(ErrorMessage = "End date can not be empty")]
        public DateTime End { get; set; }
        [Required(ErrorMessage = "Order type can not be empty")]
        public OrderType Type { get; set; }
        [Required(ErrorMessage = "Room number can not be empty")]
        public int RoomNumber { get; set; }
        public RoomDto Room { get; set; }
    }
}