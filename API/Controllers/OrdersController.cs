using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using API.Models.InputModels.Orders;
using API.Models.OutputModels.Orders;
using AutoMapper;
using DataAccessLayer.Repositories.Abstract;
using Entities.Models;
using Swashbuckle.Swagger.Annotations;

namespace API.Controllers
{
    public class OrdersController : ApiController
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrdersController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get selected order
        /// </summary>
        /// <param name="id">Order identifier</param>
        /// <returns>Order details</returns>
        [SwaggerResponse(HttpStatusCode.OK, "Order details", typeof(OrderOutputModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Empty object")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var order = await _orderRepository.GetOrderAsync(id);

            if (order == null)
                return NotFound();

            var outputOrder = _mapper.Map<OrderOutputModel>(order);

            return Ok(outputOrder);
        }

        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns>List of orders</returns>
        [SwaggerResponse(HttpStatusCode.OK, "List of orders", typeof(List<OrderOutputModel>))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Empty list")]
        public async Task<IHttpActionResult> GetAll()
        {
            var orders = await _orderRepository.GetOrdersAsync();

            if (orders == null || !orders.Any())
                return NotFound();

            var outputOrders = new List<OrderOutputModel>();

            foreach (Order order in orders)
            {
                outputOrders.Add(_mapper.Map<OrderOutputModel>(order));
            }

            return Ok(outputOrders);
        }

        /// <summary>
        /// Create new order
        /// </summary>
        /// <param name="model">Order details</param>
        /// <returns>Status</returns>
        [SwaggerResponse(HttpStatusCode.OK, "Order added")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Order is null or in invalid state")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Order addition failed")]
        public async Task<IHttpActionResult> Post(OrderInputModel model)
        {
            if (model == null)
                return BadRequest("Model can't be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = _mapper.Map<Order>(model);

            var result = await _orderRepository.SaveOrderAsync(order);
            if (!result) return InternalServerError();

            return Ok();
        }

        /// <summary>
        /// Update existing order
        /// </summary>
        /// <param name="id">Order identifier</param>
        /// <param name="model">Order details</param>
        /// <returns>Status</returns>
        [SwaggerResponse(HttpStatusCode.OK, "Order updated")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Model is null or in invalid state")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Order doesn't exist")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Order update failed")]
        public async Task<IHttpActionResult> Put(int id, OrderInputModel model)
        {
            if (model == null)
                return BadRequest("Model can't be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = await _orderRepository.GetOrderAsync(id);
            if (order == null)
                return NotFound();

            _mapper.Map(model, order);

            var result = await _orderRepository.SaveOrderAsync(order);
            if (!result) return InternalServerError();

            return Ok();
        }

        /// <summary>
        /// Delete existing order
        /// </summary>
        /// <param name="id">Order identifier</param>
        /// <returns>Status</returns>
        [SwaggerResponse(HttpStatusCode.OK, "Order deleted")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Order doesn't exist")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Order deletion failed")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var order = await _orderRepository.GetOrderAsync(id);
            if (order == null) return NotFound();

            var result = await _orderRepository.DeleteOrderAsync(order);
            if (!result) return InternalServerError();

            return Ok();
        }
    }
}