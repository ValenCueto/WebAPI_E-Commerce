using Application.Interfaces;
using Domain.Entities;
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
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController (ICartService cartService)
        {
            _cartService = cartService;
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

        private int GetAuthenticatedUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "sub");
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : -1;
            
        }

        [HttpPost("[Action]/{userId}")]
        public IActionResult CreateCart([FromRoute] int userId)
        {
            if (!IsClient())
            {
                return Forbid();
            }
            return Ok(_cartService.CreateCart(userId));
        }

        [HttpGet("[Action]/{id}")]
        public IActionResult GetCartById([FromRoute] int id)
        {
            if (!IsClient())
            {
                return Forbid();
            }
            return Ok(_cartService.GetCartById(id));
        }

        [HttpPost("[Action]/{cartId}/{productId}/{quantity}")]
        public IActionResult AddToCart([FromRoute] int cartId, [FromRoute] int productId, [FromRoute] int quantity)
        {
            var cart = _cartService.GetCartById(cartId);
            if (!IsClient())
            {
                return Forbid();
            }
            
            var authenticatedUserId = GetAuthenticatedUserId();
            if (cart.User.Id != authenticatedUserId)
            {
               return BadRequest();
            }
            return Ok(_cartService.AddToCart(cartId, productId, quantity));
        }

        [HttpDelete("[Action]/{cartId}/{productId}")]
        public IActionResult RemoveFromCart([FromRoute] int cartId, [FromRoute] int productId)
        {
            if (!IsClient())
            {
                return Forbid();
            }
            return Ok(_cartService.RemoveFromCart(cartId, productId));
        }

        [HttpPut("[Action]/{cartId}/{productId}/{newQuantity}")]
        public IActionResult UpdateCart([FromRoute] int cartId, [FromRoute] int productId, [FromRoute] int newQuantity)
        {
            if (!IsClient())
            {
                return Forbid();
            }
            return Ok(_cartService.UpdateCart(cartId, productId, newQuantity)); 
        }



    }
}
