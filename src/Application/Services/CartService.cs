using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class CartService
    {
        private readonly ICartRepository _cartRepository;

        private readonly IProductRepository _productRepository;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public Cart? GetCartById(int cartId)
        {
            return _cartRepository.GetById(cartId);
        }
        public float AddToCart(int cartId, int productId, int quantity)
        {
            var cart = _cartRepository.GetById(cartId);
            if (cart == null )
            {
                throw new Exception($"El carrito con ID {cartId} no fue encontrado.");
            }
            var product = _productRepository.GetById(productId);
            if (product == null) 
            {
                throw new Exception($"El producto con ID {productId} no fue encontrado.");
            }
            var newDetail = new CartDetail
            {
                Cart = cart,
                Product = product,
                Quantity = quantity,
            };
            cart.Details.Add(newDetail);
            cart.TotalPrice = cart.Details.Sum(d => d.Product.Price * d.Quantity);
            _cartRepository.Update(cart);
            return cart.TotalPrice;
        }
        public float RemoveFromCart(int cartId, int productId)
        {
            var cart = _cartRepository.GetById(cartId);
            if (cart == null)
            {
                throw new Exception($"El carrito con ID {cartId} no fue encontrado.");
            }
            var detail = _cartRepository.GetDetailByProduct(cartId, productId);
            _cartRepository.RemoveFromCart(cartId, productId);
            cart.TotalPrice = cart.Details.Sum(d => d.Product.Price * d.Quantity);
            _cartRepository.Update(cart);
            return cart.TotalPrice;
        }




    }

}
