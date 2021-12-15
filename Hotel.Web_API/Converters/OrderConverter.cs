using System;
using Hotel.DAL.Entities;
using Hotel.Web_API.Models;

namespace Hotel.Web_API.Converter
{
    public class OrderConverter
    {
        public static OrderDto Order2Dto(Order order)
        {
            return new OrderDto
            {
                OrderNumber = order.Id,
                Start = order.Start,
                End = order.End,
                Type = order.Type,
                Room = RoomConverter.Room2Dto(order.Room)
            };
        }

        internal static Order Dto2Order(OrderDto orderDto)
        {
            return new Order
            {
                Start = orderDto.Start,
                End = orderDto.End,
                Type = orderDto.Type,
                RoomId = orderDto.RoomNumber
            };
        }
    }
}