using Hotel.DAL.Entities;
using System;

namespace Hotel.Web_API.Models
{
    public class OrderDto
    {
        public int OrderNumber { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public OrderType Type { get; set; }
        public RoomDto Room { get; set; }
    }
}