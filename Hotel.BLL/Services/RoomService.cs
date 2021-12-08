using Hotel.BLL.interfaces;
using Hotel.BLL.Validation;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;

namespace Hotel.BLL.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(Room room)
        {
            var roomCategory = _unitOfWork.RoomCategories.FindById(room.RoomCategoryId);

            if (roomCategory == null)
            {
                throw new HotelException("Room category with id = " + room.RoomCategoryId + " was not found");
            }

            _unitOfWork.Rooms.Create(room);
            _unitOfWork.Save();
        }

        public Room FindById(int id)
        {
            return _unitOfWork.Rooms.FindById(id);
        }

        public bool IsExistById(int id)
        {
            return FindById(id) != null;
        }

        public void Update(int roomId, Room newRoom)
        {
            var isRoomExists = IsExistById(roomId);
            if (!isRoomExists)
            {
                throw new HotelException("There is no room room with id = " + roomId);
            }

            var roomCategory = _unitOfWork.RoomCategories.FindById(newRoom.RoomCategoryId);
            var room = _unitOfWork.Rooms.FindById(roomId);
            room.RoomCategory = roomCategory ?? 
                throw new HotelException("Room category with id = " + newRoom.RoomCategoryId + " was not found");

            _unitOfWork.Rooms.Update(room);
            _unitOfWork.Save();
        }

        public void DeleteById(int id)
        {
            var isRoomExists = IsExistById(id);
            if (!isRoomExists)
            {
                throw new HotelException("There is no room room with id = " + id);
            }

            _unitOfWork.Rooms.Delete(id);
            _unitOfWork.Save();
        }
    }
}
