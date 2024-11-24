using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Taller1_WebMovil.Src.DTOs.ShoppingCart;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Mapper
{
    public static class ShoppingCartMapper
    {
        public static ShoppingCartInfoDto ToShoppingCartInfoDto(this CartItem cartItem)
    {
        return new ShoppingCartInfoDto
        {
            productId = cartItem.productId.ToString(),
            productName = cartItem.Product?.name,
            quantity = cartItem.quantity.ToString()
        };
    } 
    }
}