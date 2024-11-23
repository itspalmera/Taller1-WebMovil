using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taller1_WebMovil.Src.DTOs;

namespace Taller1_WebMovil.Src.Interface
{
    public interface ICartItemRepository
    {
        Task<bool> AddToCart(string userId,CartItemDto cartItemDto);
    }
}