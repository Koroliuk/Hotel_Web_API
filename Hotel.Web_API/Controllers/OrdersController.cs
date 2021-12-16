using Hotel.BLL.interfaces;
using Hotel.BLL.Validation;
using Hotel.DAL.Entities;
using Hotel.Web_API.Converter;
using Hotel.Web_API.Models;
using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Hotel.Web_API.Controllers
{
    /// <summary>
    ///     Represents a orders
    /// </summary>
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

        /// <summary>
        ///     Return a list of orders
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(IList<OrderDto>))]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Sucessfuly returned a list of orders")]
        // GET: api/Orders
        public IHttpActionResult Get()
        {
            var orders = _orderService.GetAll()
                           .Select(order => OrderConverter.Order2Dto(order));
             return Ok(orders);
        }

        /// <summary>
        ///     Create a new order
        /// </summary>
        /// <param name="orderDto">orderDto model</param>
        /// <returns></returns>
        [SwaggerResponseRemoveDefaults]
        [ResponseType(typeof(OrderDto))]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Sucessfuly create an order")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Invalid input date (request body)")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Server error")]
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

        /// <summary>
        ///     Update an order
        /// </summary>
        /// <param name="id">order number and identifier</param>
        /// <returns></returns>
        [ResponseType(typeof(OrderDto))]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Sucessfuly update an order")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Invalid order number (identifier)")]
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

        /// <summary>
        ///     Dekete an order
        /// </summary>
        /// <param name="id">order number and identifier</param>
        /// <returns></returns>
        [ResponseType(typeof(OrderDto))]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Sucessfuly delete an order")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Invalid order number (identifier)")]
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
