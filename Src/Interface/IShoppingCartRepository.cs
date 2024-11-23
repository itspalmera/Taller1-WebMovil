using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Interface
{
    public interface IShoppingCartRepository
    {
       Task<IEnumerable<ShoppingCart?>> GetCartItems(string userId);
       Task<CartItem?> GetCartItemByProduct(string userId,int productId);
       Task<bool> DeleteCartItem(string userId,int productId);
    }
}