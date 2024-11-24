using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taller1_WebMovil.Src.DTOs;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Mapper
{
    public static class CartItemMapper
    {
        public static CartItem ToCartItem(this CartItemDto cartItemDto){

            return new CartItem
        {
            quantity = cartItemDto.quantity,
            productId = cartItemDto.productId
        };
        }
    }
}