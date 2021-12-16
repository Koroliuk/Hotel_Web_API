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
                    .ForMember("OrderNumber", (IMemberConfigurationExpression<Order, OrderDto, object> opt) => opt.MapFrom(p => p.Id))
                    .ForMember("RoomNumber", (IMemberConfigurationExpression<Order, OrderDto, object> opt) => opt.MapFrom(p => p.RoomId));
                cfg.CreateMap<Room, RoomDto>()
                    .ForMember("RoomNumber", (IMemberConfigurationExpression<Room, RoomDto, object> opt) => opt.MapFrom(p => p.Id));
                cfg.CreateMap<RoomCategory, RoomCategoryDto>()
                    .ForMember("CategoryNumber", (IMemberConfigurationExpression<RoomCategory, RoomCategoryDto, object> opt) => opt.MapFrom(p => p.Id));

                cfg.CreateMap<OrderDto, Order>()
                    .ForMember("Id", (IMemberConfigurationExpression<OrderDto, Order, object> opt) => opt.MapFrom(p => p.OrderNumber))
                    .ForMember("RoomId", (IMemberConfigurationExpression<OrderDto, Order, object> opt) => opt.MapFrom(p => p.RoomNumber));
                cfg.CreateMap<RoomDto, Room>()
                    .ForMember("Id", (IMemberConfigurationExpression<RoomDto, Room, object> opt) => opt.MapFrom(p => p.RoomNumber));
                cfg.CreateMap<RoomCategoryDto, RoomCategory>()
                     .ForMember("Id", (IMemberConfigurationExpression<RoomCategoryDto, RoomCategory, object> opt) => opt.MapFrom(p => p.CategoryNumber));
                
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }
    }
}