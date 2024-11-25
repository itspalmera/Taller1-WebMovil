using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Taller1_WebMovil.Src.DTOs.ShoppingCart;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Mapper
{

    /// <summary>
    /// A static class to map objects related to the shopping cart.
    /// </summary>
    public static class ShoppingCartMapper
    {

        /// <summary>
        /// Maps a <see cref="CartItem"/> object to a <see cref="ShoppingCartInfoDto"/> object.
        /// </summary>
        /// <param name="cartItem">The <see cref="CartItem"/> object to be mapped.</param>
        /// <returns>A <see cref="ShoppingCartInfoDto"/> object containing the mapped data.</returns>
        public static ShoppingCartInfoDto ToShoppingCartInfoDto(this CartItem cartItem)
        {
        return new ShoppingCartInfoDto
        {
            productId = cartItem.productId.ToString(),
            productName = cartItem.Product?.name,
            quantiy = cartItem.quantity.ToString()
        };
        } 
    }
}