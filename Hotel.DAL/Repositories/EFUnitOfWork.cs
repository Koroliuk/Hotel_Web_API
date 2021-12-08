using System;
using Hotel.DAL.EF;
using Hotel.DAL.Interfaces;

namespace Hotel.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly HotelContext _context;
        private UserRepository _userRepository;
        private RoomRepository _roomRepository;
        private RoomCategoryRepository _roomCategoryRepository;
        private OrderRepository _orderRepository;

        public EFUnitOfWork()
        {
            _context = new HotelContext();
        }

        public IUserRepository Users
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }
                return _userRepository;
            }
        }

        public IOrderRepository Orders
        {
            get
            {
                if (_orderRepository == null)
                {
                    _orderRepository = new OrderRepository(_context);
                }
                return _orderRepository;
            }
        }

        public IRoomRepository Rooms
        {
            get
            {
                if (_roomRepository == null)
                {
                    _roomRepository = new RoomRepository(_context);
                }
                return _roomRepository;
            }
        }

        public IRoomCategoryRepository RoomCategories
        {
            get
            {
                if (_roomCategoryRepository == null)
                {
                    _roomCategoryRepository = new RoomCategoryRepository(_context);
                }
                return _roomCategoryRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
