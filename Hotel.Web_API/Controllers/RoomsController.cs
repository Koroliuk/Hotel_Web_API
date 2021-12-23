using AutoMapper;
using Hotel.BLL.interfaces;
using Hotel.BLL.Validation;
using Hotel.DAL.Entities;
using Hotel.Web_API.App_Start;
using Hotel.Web_API.Models;
using Hotel.Web_API.Utils;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace Hotel.Web_API.Controllers
{
    /// <summary>
    ///     Represent rooms
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RoomsController : ApiController
    {
        private readonly IMapper mapper = AutoMapperConfiguration.provideMaper();
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

        /// <summary>
        /// Return a list of free rooms for a range of time
        /// </summary>
        /// <param name="start">Start point of input time range</param>
        /// <param name="end">End point of input time range</param>
        /// <returns></returns>
        [ResponseType(typeof (IList<RoomDto>))]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Sucessfuly returned a list of rooms")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Invalid data")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Server error")]
        // GET: api/Rooms
        public IHttpActionResult Get(string start=null, string end=null)
        {
            bool isStartDateStringEmpty = StringUtils.IsBlank(start);
            bool isEndDateStringEmpty = StringUtils.IsBlank(end);

            if (isStartDateStringEmpty && isEndDateStringEmpty)
            {
                return BadRequest("Start or end dates must be specified");
            }
            try
            {
                var startDate = isStartDateStringEmpty ? DateTime.Now : DateTime.Parse(start);
                var endDate = isEndDateStringEmpty ? DateTime.MaxValue : DateTime.Parse(end);
                var rooms = _orderService.GetFreeRooms(startDate, endDate)
                    .Select(room => mapper.Map<Room, RoomDto>(room));
                return Ok(rooms);
            }
            catch (HotelException e)
            {
                return BadRequest(e.Message);
            }
            catch (FormatException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return InternalServerError();
            }
        }

        /// <summary>
        ///     Returns a room
        /// </summary>
        /// <param name="id">Room identifier and number</param>
        /// <returns></returns>
        [ResponseType(typeof (RoomDto))]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Sucessfuly returned a room")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "There is no room with a such id")]
        // GET: api/Rooms/5
        public IHttpActionResult Get(int id)
        {
            var room = _roomService.FindById(id);
            if (room == null)
            {
                return BadRequest("There is no such a room");
            }
            var roomDto = mapper.Map<Room, RoomDto>(room);
            return Ok(roomDto);
        }
    }
}
