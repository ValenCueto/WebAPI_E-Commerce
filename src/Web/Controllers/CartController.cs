using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController (ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("[Action]/{id}")]
        public IActionResult GetCartById([FromRoute] int id)
        {
            return Ok(_cartService.GetCartById(id));
        }

        [HttpPost("[Action]/{cartId}/{productId}/{quantity}")]
        public IActionResult AddToCart([FromRoute] int cartId, [FromRoute] int productId, [FromRoute] int quantity)
        {
            return Ok(_cartService.AddToCart(cartId, productId, quantity));
        }

        [HttpDelete("[Action]/{cartId}/{productId}")]
        public IActionResult RemoveFromCart([FromRoute] int cartId, [FromRoute] int productId)
        {
            return Ok(_cartService.RemoveFromCart(cartId, productId));
        }

        [HttpPut("[Action]/{cartId}/{productId}/{newQuantity}")]
        public IActionResult UpdateCart([FromRoute] int cartId, [FromRoute] int productId, [FromRoute] int newQuantity)
        {
            return Ok(_cartService.UpdateCart(cartId, productId, newQuantity)); 
        }



    }
}
