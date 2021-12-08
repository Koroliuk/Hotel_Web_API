using Hotel.DAL.Entities;

namespace Hotel.BLL.interfaces
{
    public interface IRoomService
    {
        void Create(Room room);
        Room FindById(int id);
        bool IsExistById(int id);
        void Update(int roomId, Room room);
        void DeleteById(int id);
    }
}
