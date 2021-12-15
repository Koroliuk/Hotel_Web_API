using System;
using System.Collections.Generic;
using System.Linq;
using Hotel.BLL.interfaces;
using Hotel.BLL.Validation;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;

namespace Hotel.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Order BookRoomById(int roomId, User user, DateTime startDate, DateTime endDate)
        {
            if (startDate.Date < DateTime.Now.Date || startDate > endDate)
            {
                throw new HotelException("Start date should be bigger or equal now and be less that end date");
            }

            var room = _unitOfWork.Rooms.FindById(roomId);
            if (room == null)
            {
                throw new HotelException("There is no a such room");
            }

            var freeRooms = GetFreeRooms(startDate, endDate);

            if (!freeRooms.Contains(room))
            {
                throw new HotelException("A such room is not available at this range of time");
            }

            var order = new Order
            {
                Start = startDate,
                End = endDate,
                Type = OrderType.Booked,
                User = user,
                Room = room
            };

            _unitOfWork.Orders.Create(order);
            _unitOfWork.Save();

            return _unitOfWork.Orders
                .Find(o => o.Start == startDate && o.End == endDate && o.Room.Id == roomId)
                .First();
        }

        public Order RentRoomById(int roomId, User user, DateTime startDate, DateTime endDate)
        {
            if (startDate.Date < DateTime.Now.Date || startDate > endDate)
            {
                throw new HotelException("Start date should be bigger or equal now and be less that end date");
            }

            var room = _unitOfWork.Rooms.FindById(roomId);
            if (room == null)
            {
                throw new HotelException("There is no a such room");
            }

            var freeRooms = GetFreeRooms(startDate, endDate);

            if (!freeRooms.Contains(room))
            {
                throw new HotelException("A such room is not available at this range of time");
            }

            var order = new Order
            {
                Start = startDate,
                End = endDate,
                Type = OrderType.Paid,
                User = user,
                Room = room
            };

            _unitOfWork.Orders.Create(order);
            _unitOfWork.Save();

            return _unitOfWork.Orders
                .Find(o => o.Start == startDate && o.End == endDate && o.Room.Id == roomId)
                .First();
        }

        public Order TransformFromBookedToRentedById(int id)
        {
            var order = FindById(id);

            if (order == null)
            {
                throw new HotelException("There is no such order");
            }

            order.Type = OrderType.Paid;

            _unitOfWork.Orders.Update(order);
            _unitOfWork.Save();

            return order;
        }

        public IEnumerable<Room> GetFreeRooms(DateTime startDate, DateTime endDate)
        {
            if (startDate.Date < DateTime.Now.Date || startDate > endDate)
            {
                throw new HotelException("Start date should be bigger or equal now and be less that end date");
            }

            var occupiedRooms = _unitOfWork.Orders.Find(o => o.Start >= startDate && o.End <= endDate)
                .Select(o => o.Room)
                .Distinct();

            return _unitOfWork.Rooms.GetAll().Where(r => !occupiedRooms.Contains(r));
        }

        public Order FindById(int id)
        {
            return _unitOfWork.Orders.FindById(id);
        }

        public bool IsExistsById(int id)
        {
            return FindById(id) != null;
        }

        public IEnumerable<Order> GetAll()
        {
            return _unitOfWork.Orders.GetAll();
        }

        public void DeleteById(int id)
        {
            _unitOfWork.Orders.Delete(id);
            _unitOfWork.Save();
        }

        public Order Save(Order order)
        {
            var startDate = order.Start.Date;
            var endDate = order.End.Date;

            if (startDate < DateTime.Now.Date || startDate > endDate)
            {
                throw new HotelException("Start date should be bigger or equal now and be less that end date");
            }

            var room = _unitOfWork.Rooms.FindById(order.RoomId);
            if (room == null)
            {
                throw new HotelException("There is no a such room");
            }

            var freeRooms = GetFreeRooms(startDate, endDate);

            if (!freeRooms.Contains(room))
            {
                throw new HotelException("A such room is not available at this range of time");
            }

            order.Room = room;
            _unitOfWork.Orders.Create(order);
            _unitOfWork.Save();

            return _unitOfWork.Orders
               .Find(o => o.Start == order.Start && o.End == order.End && o.Room.Id == order.Room.Id)
               .First();
        }
    }
}
