using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository, IUserRepository userRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        public CartToResponse? CreateCart(int userId)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
            {
                throw new Exception("No se ha encontrado el usuario");
            }
            var existingCart = _cartRepository.GetCartByUserId(userId);
            if(existingCart is not null)
            {
                throw new Exception("ya existe un carrito para este usuario");
            }
            var cart = new Cart();
            cart.SetUser(user);
            _cartRepository.Create(cart);

            var userToResponse = new UserToResponse()
            {
                Id = cart.User.Id,
                Name = cart.User?.Name,
                Email = cart.User.Email
            };

            var cartToResponse = new CartToResponse()
            {
                Id = cart.Id,
                TotalPrice = cart.TotalPrice,
                User = userToResponse,
                Details = cart.Details
            };
            return cartToResponse;
        }
        public CartToResponse? GetCartById(int cartId)
        {
            var cart = _cartRepository.GetCartById(cartId);
            if(cart == null)
            {
                throw new Exception("no se ha encontrado el carrito");
            }
            
            var userToResponse = new UserToResponse()
            {
                Id = cart.User.Id,
                Name = cart.User?.Name,
                Email = cart.User.Email
            };

            var cartToResponse = new CartToResponse()
            {
                Id = cart.Id,
                TotalPrice = cart.TotalPrice,
                User = userToResponse,
                Details = cart.Details
            };
            return cartToResponse;
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
            if (product.Stock == 0)
            {
                throw new Exception("No hay stock del producto");
            }
            if (quantity > product.Stock) 
            {
                throw new Exception("No hay stock suficiente");
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
        public CartToResponse? RemoveFromCart(int cartId, int productId)
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

            var userToResponse = new UserToResponse()
            {
                Id = cart.User.Id,
                Name = cart.User?.Name,
                Email = cart.User.Email

            };

            var cartToResponse = new CartToResponse()
            {
                Id = cart.Id,
                TotalPrice = cart.TotalPrice,
                User = userToResponse,
                Details = cart.Details
            };
            return cartToResponse;
        }

        public CartToResponse? UpdateCart(int cartId, int productId, int newQuantity)
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

            var userToResponse = new UserToResponse()
            {
                Id = cart.User.Id,
                Name = cart.User?.Name,
                Email = cart.User.Email
            };

            var cartToResponse = new CartToResponse()
            {
                Id = cart.Id,
                TotalPrice = cart.TotalPrice,
                User = userToResponse,
                Details = cart.Details
            };
            return cartToResponse;
        }




    }

}
