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
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = e.Message });
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                    new { Message = "Please, check input dates" });
            }
        }

        // PUT: api/Orders/5
        public IHttpActionResult Put(int id)
        {
            var order = _roomService.FindById(id);
            if (order == null)
            {
                return BadRequest("There is no such a order");
            }
            var newOrder = _orderService.TransformFromBookedToRentedById(id);
            var newOrderDto = OrderConverter.Order2Dto(newOrder);
            return Ok(newOrderDto);
        }

        // DELETE: api/Orders/5
        public IHttpActionResult Delete(int id)
        {
            var order = _roomService.FindById(id);
            if (order == null)
            {
                return NotFound();
            }
            _orderService.DeleteById(id);
            return Ok();
        }
    }
}
