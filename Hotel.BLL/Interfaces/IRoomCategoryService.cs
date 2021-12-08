using Hotel.DAL.Entities;

namespace Hotel.BLL.interfaces
{
    public interface IRoomCategoryService
    {
        void Create(RoomCategory roomCategory);

        RoomCategory FindById(int id);
        bool IsExistById(int id);
        void Update(int categoryId, RoomCategory roomCategory);
        void DeleteById(int id);
    }
}
