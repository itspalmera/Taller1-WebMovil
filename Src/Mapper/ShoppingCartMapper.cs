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
        public static ShoppingCartInfoDto toShoppingCartDto(this ShoppingCart shoppingCart){
            return new ShoppingCartInfoDto{
            productId = shoppingCart.cartItem.productId.ToString(),
            productName =shoppingCart.cartItem.product.name.ToString(),
            quantiy = shoppingCart.cartItem.quantity.ToString()
            };
        }
        public static ShoppingCart toShoppingCart(this ShoppingCartDto shoppingCartDto){
            return new ShoppingCart{
                userId = shoppingCartDto.userId,
                cartItemId = shoppingCartDto.cartItemId
            };
        }
    }
}