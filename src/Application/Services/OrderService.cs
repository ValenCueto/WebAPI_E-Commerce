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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;

        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
        }
        public OrderToResponse GetOrderById(int orderId)
        {
            var order = _orderRepository.GetOrderById(orderId);
            if (order == null)
            {
                throw new Exception($"No se encontro la orden con ID: {orderId}");
            }
            var userToResponse = new UserToResponse()
            {
                Id = order.Client.Id,
                Name = order.Client.Name
            };

            var orderToResponse = new OrderToResponse()
            {
                Id = order.Id,
                TotalPrice = order.TotalPrice,
                User = userToResponse,
                Details = order.Details,
            };
            return orderToResponse;
        }
        public int CreateOrderFromCart(int cartId)
        {
            var cart = _cartRepository.GetCartById(cartId);
            if (cart == null)
            {
                throw new Exception($"Carrito con ID {cartId} no encontrado.");
            }
            var order = new Order
            {
                Client = cart.User,
                TotalPrice = cart.TotalPrice,
                Details = new List<OrderDetail>()
            };

            foreach (var cartDetail in cart.Details)
            {
                var orderDetail = new OrderDetail
                {
                    Product = cartDetail.Product,
                    Quantity = cartDetail.Quantity,
                    Order = order
                };
                order.Details.Add(orderDetail);
            }
            _orderRepository.Create(order);

            cart.Details.Clear();
            cart.TotalPrice = 0;

      
            _cartRepository.Update(cart);
            return order.Id;
        }

        public void DeleteOrderFromCart(int orderId)
        {
           
            var order = _orderRepository.GetOrderById(orderId);
            if (order == null)
            {
                throw new Exception($"La orden con ID: {orderId} no se encontró");
            }
            _orderRepository.Delete(order);
        }
    }




}
