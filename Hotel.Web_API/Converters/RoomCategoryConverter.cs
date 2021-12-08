using Hotel.DAL.Entities;
using Hotel.Web_API.Models;

namespace Hotel.Web_API.Converter
{
    public class RoomCategoryConverter
    {
        public static RoomCategoryDto RoomCagetory2Dto(RoomCategory roomCagetory)
        {
            return new RoomCategoryDto
            {
                CategoryNumber = roomCagetory.Id,
                Name = roomCagetory.Name,
                Description = roomCagetory.Description,
                Capacity = roomCagetory.Capacity,
                PricePerDay = roomCagetory.PricePerDay
            };
        }
    }
}