using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICartService
    {
        Cart? GetCartById(int cartId);
        float AddToCart(int cartId, int productId, int quantity);
        Cart RemoveFromCart(int cartId, int productId);
        Cart UpdateCart(int cartId, int productId, int newQuantity);





    }
}
