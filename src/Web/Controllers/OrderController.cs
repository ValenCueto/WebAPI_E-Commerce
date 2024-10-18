using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService) 
        {
            _orderService = orderService;
        }

        private bool IsSeller()
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == RolEnum.Seller.ToString())
            {
                return true;
            }
            return false;
        }

        private bool IsClient()
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == RolEnum.Client.ToString())
            {
                return true;
            }
            return false;
        }

        [HttpGet("[Action]/{orderId}")]
        public IActionResult GetById([FromRoute] int orderId)
        {
            if (!IsSeller())
            {
                return Forbid();
            }
            return Ok(_orderService.GetOrderById(orderId));
        }

        [HttpPost("[Action]/{cartId}")]
        public IActionResult CreateOrder([FromRoute] int cartId)
        {
            if (!IsClient())
            {
                return Forbid();
            }
            return Ok(_orderService.CreateOrderFromCart(cartId));
        }

        [HttpDelete("[Action]/{orderId}")]
        public IActionResult DeleteOrder(int orderId) 
        {
            if (!IsSeller())
            {
                return Forbid();
            }
            _orderService.DeleteOrderFromCart(orderId);
            return Ok();
        }
    }
}
