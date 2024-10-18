using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService) 
        {
            _orderService = orderService;
        }

        [HttpGet("[Action]")]
        public IActionResult GetAll()
        {
            return Ok(_orderService.GetAll());
        }

        [HttpPost("[Action]/{cartId}")]
        public IActionResult CreateOrder([FromRoute] int cartId)
        {
            return Ok(_orderService.CreateOrderFromCart(cartId));
        }

        [HttpDelete("[Action]/{orderId}")]
        public IActionResult DeleteOrder(int orderId) 
        {
            _orderService.DeleteOrderFromCart(orderId);
            return Ok();
        }
    }
}
