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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;

        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
        }
        public List<Order> GetAll()
        {
            return _orderRepository.GetAll();
        }
        public Order CreateOrderFromCart(int cartId)
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
            return order;
        }

        public void DeleteOrderFromCart(int orderId)
        {
           
            var order = _orderRepository.GetById(orderId);
            if (order == null)
            {
                throw new Exception($"La orden con ID: {orderId} no se encontró");
            }
            _orderRepository.Delete(order);
        }
    }




}
