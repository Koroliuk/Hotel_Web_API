using Hotel.DAL.Entities;
using Hotel.Web_API.Models;

namespace Hotel.Web_API.Converter
{
    public class RoomConverter
    {
        public static RoomDto Room2Dto(Room room)
        {
            return new RoomDto
            {
                RoomNumber = room.Id,
                Category = RoomCategoryConverter.RoomCagetory2Dto(room.RoomCategory)
            };
        }
    }
}