using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class CartService : ICartService
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
        public Cart RemoveFromCart(int cartId, int productId)
        {
            var cart = _cartRepository.GetCartWithDetails(cartId);
            if (cart == null)
            {
                throw new Exception($"El carrito con ID {cartId} no fue encontrado.");
            }
            if (cart.Details == null)
            {
                throw new Exception($"El carrito con ID {cartId} no posee detalles");
            }
            CartDetail? detailToDelete = null;
            foreach (var d in cart.Details)
            {
                if (d.Product?.Id == productId)
                {
                    detailToDelete = d;
                    break;
                }
            }
            if (detailToDelete == null)
            {
                throw new Exception($"No existe ningun producto para el ID {productId}");
            }
            else
            {
                cart.Details.Remove(detailToDelete);
            }
            cart.TotalPrice = cart.Details.Sum(t => t.Product.Price * t.Quantity);
            _cartRepository.Update(cart);
            return cart;
        }

        public Cart UpdateCart(int cartId, int productId, int newQuantity)
        {
            var cart = _cartRepository.GetCartWithDetails(cartId);
            if (cart == null)
            {
                throw new Exception($"El carrito con ID {cartId} no fue encontrado.");
            }
            if (cart.Details == null)
            {
                throw new Exception($"El carrito con ID {cartId} no posee detalles");
            }
            CartDetail? detailToUpdate = null;
            foreach (var d in cart.Details)
            {
                if (d.Product?.Id == productId)
                {
                    detailToUpdate = d;
                    break;
                }
            }
            if (detailToUpdate == null)
            {
                throw new Exception($"No existe ningun producto para el ID {productId}");
            }
            else
            {
                detailToUpdate.Quantity = newQuantity;
            }
            cart.TotalPrice = cart.Details.Sum(t => t.Product.Price * t.Quantity);
            _cartRepository.Update(cart);
            return cart;
        }




    }

}
