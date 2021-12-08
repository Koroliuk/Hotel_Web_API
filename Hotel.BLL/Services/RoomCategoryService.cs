using Hotel.BLL.interfaces;
using Hotel.BLL.Validation;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;

namespace Hotel.BLL.Services
{
    public class RoomCategoryService : IRoomCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(RoomCategory roomCategory)
        {
            if (roomCategory.Name == null || roomCategory.Name.Equals(string.Empty) ||
                roomCategory.Description == null || roomCategory.Capacity <= 0 ||
                roomCategory.PricePerDay <= 0)
            {
                throw new HotelException("Invalid input");
            }

            _unitOfWork.RoomCategories.Create(roomCategory);
            _unitOfWork.Save();
        }

        public RoomCategory FindById(int id)
        {
            return _unitOfWork.RoomCategories.FindById(id);
        }

        public bool IsExistById(int id)
        {
            return FindById(id) != null;
        }

        public void Update(int categoryId, RoomCategory newRoomCategory)
        {
            if (newRoomCategory.Name == null || newRoomCategory.Name.Equals(string.Empty) ||
                newRoomCategory.Description == null || newRoomCategory.Capacity == 0 ||
                newRoomCategory.PricePerDay == 0)
            {
                throw new HotelException("Invalid input");
            }

            var roomCategory = FindById(categoryId);
            roomCategory.Name = newRoomCategory.Name;
            roomCategory.PricePerDay = newRoomCategory.PricePerDay;
            roomCategory.Capacity = newRoomCategory.Capacity;
            roomCategory.Description = newRoomCategory.Description;

            _unitOfWork.RoomCategories.Update(roomCategory);
            _unitOfWork.Save();
        }

        public void DeleteById(int id)
        {
            var isCategoryExists = IsExistById(id);
            if (!isCategoryExists)
            {
                throw new HotelException("There is no room category with id = " + id);
            }
            _unitOfWork.RoomCategories.Delete(id);
            _unitOfWork.Save();
        }
    }
}
