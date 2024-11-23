using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taller1_WebMovil.Src.DTOs.ShoppingCart;

namespace Taller1_WebMovil.Src.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<ShoppingCartInfoDto?>> GetCartItems(string userId);
        Task<bool> DeleteCartItem(string userId,int id);
    }
}