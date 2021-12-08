using Hotel.BLL.interfaces;
using Hotel.BLL.Validation;
using Hotel.Web_API.Converter;
using Hotel.Web_API.Models;
using Hotel.Web_API.Utils;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Hotel.Web_API.Controllers
{
    public class OrdersController : ApiController
    {
        private readonly IOrderService _orderService;
        private readonly IRoomService _roomService;

        public OrdersController(IOrderService orderService, IRoomService roomService) : base()
        {
            _orderService = orderService;
            _roomService = roomService;
        }

        public OrdersController()
        {
        }

        // GET: api/Orders
        public IHttpActionResult Get()
        {
            var orders = _orderService.GetAll()
                           .Select(order => OrderConverter.Order2Dto(order));
            return Ok(orders);
        }

        // POST: api/Orders
        [ResponseType(typeof(OrderDto))]
        public HttpResponseMessage Post([FromBody]int roomId, [FromBody]string startDateString, [FromBody]string endDateString, 
            [FromBody]bool isPaid = false)
        {
            if (StringUtils.IsBlank(startDateString) || StringUtils.IsBlank(endDateString))
            {
                //var message = "Please provide input dates";
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            try
            {
                var startDate = DateTime.Parse(startDateString);
                var endDate = DateTime.Parse(endDateString);
                //OrderDto orderDto = null;
                if (!isPaid)
                {
                    _orderService.BookRoomById(roomId, null, startDate, endDate);
                }
                else
                {
                    _orderService.RentRoomById(roomId, null, startDate, endDate);

                }
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch (HotelException)
            {
                //var message = e.Message;
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            catch
            {
                //var message = "Please, check input dates";
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // PUT: api/Orders/5
        public IHttpActionResult Put(int id)
        {
            _orderService.TransformFromBookedToRentedById(id);
            return Ok();
        }

        // DELETE: api/Orders/5
        public IHttpActionResult Delete(int id)
        {
            _orderService.DeleteById(id);
            return Ok();
        }
    }
}
