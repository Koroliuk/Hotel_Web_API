using Hotel.BLL.interfaces;
using Hotel.BLL.Validation;
using Hotel.Web_API.Converter;
using Hotel.Web_API.Utils;
using System;
using System.Linq;
using System.Web.Http;

namespace Hotel.Web_API.Controllers
{
    public class RoomsController : ApiController
    {
        private readonly IOrderService _orderService;
        private readonly IRoomService _roomService;

        public RoomsController(IOrderService orderService, IRoomService roomService) : base()
        {
            _orderService = orderService;
            _roomService = roomService;
        }

        public RoomsController()
        {
        }

        // GET: api/Rooms
        public IHttpActionResult Get(string startDateString=null, string endDateString=null)
        {
            bool isStartDateStringEmpty = StringUtils.IsBlank(startDateString);
            bool isEndDateStringEmpty = StringUtils.IsBlank(endDateString);

            if (isStartDateStringEmpty && isEndDateStringEmpty)
            {
                return BadRequest();
            }
            try
            {
                var startDate = isStartDateStringEmpty ? DateTime.Now : DateTime.Parse(startDateString);
                var endDate = isEndDateStringEmpty ? DateTime.MaxValue : DateTime.Parse(endDateString);
                var rooms = _orderService.GetFreeRooms(startDate, endDate)
                    .Select(room => RoomConverter.Room2Dto(room));
                return Ok(rooms);
            }
            catch (HotelException e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/Rooms/5
        public IHttpActionResult Get(int id)
        {
            var room = _roomService.FindById(id);
            var roomDto = RoomConverter.Room2Dto(room);
            return Ok(roomDto);
        }
    }
}
