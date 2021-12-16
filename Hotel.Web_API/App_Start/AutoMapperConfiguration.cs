using AutoMapper;
using Hotel.DAL.Entities;
using Hotel.Web_API.Models;

namespace Hotel.Web_API.App_Start
{
    public class AutoMapperConfiguration
    {
        public static IMapper provideMaper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Order, OrderDto>()
                    .ForMember("OrderNumber", opt => opt.MapFrom(p => p.Id))
                    .ForMember("RoomNumber", opt => opt.MapFrom(p => p.RoomId));
                cfg.CreateMap<Room, RoomDto>()
                    .ForMember("RoomNumber", opt => opt.MapFrom(p => p.Id));
                cfg.CreateMap<RoomCategory, RoomCategoryDto>()
                    .ForMember("CategoryNumber", opt => opt.MapFrom(p => p.Id));

                cfg.CreateMap<OrderDto, Order>()
                    .ForMember("Id", opt => opt.MapFrom(p => p.OrderNumber))
                    .ForMember("RoomId", opt => opt.MapFrom(p => p.RoomNumber));
                cfg.CreateMap<RoomDto, Room>()
                    .ForMember("Id", opt => opt.MapFrom(p => p.RoomNumber));
                cfg.CreateMap<RoomCategoryDto, RoomCategory>()
                     .ForMember("Id", opt => opt.MapFrom(p => p.CategoryNumber));
                
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }
    }
}
