using Hotel.BLL.interfaces;
using Hotel.BLL.Validation;
using Hotel.DAL.Entities;
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
        public HttpResponseMessage Post([FromBody] OrderDto orderDto)
        {
            try
            {
                Order order = OrderConverter.Dto2Order(orderDto);
                orderDto = OrderConverter.Order2Dto(_orderService.Save(order));
                return Request.CreateResponse(HttpStatusCode.Created, orderDto);
            }
            catch (HotelException e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch
            {
                var message = "Please, check input dates";
                return Request.CreateResponse(HttpStatusCode.BadRequest, message);
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
