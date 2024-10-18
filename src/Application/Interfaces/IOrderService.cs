using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        OrderToResponse GetOrderById(int orderId);
        int CreateOrderFromCart(int cartId);
        void DeleteOrderFromCart(int orderId);

    }
}
