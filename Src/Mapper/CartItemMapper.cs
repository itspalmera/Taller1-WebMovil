using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taller1_WebMovil.Src.DTOs;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Mapper
{

    /// <summary>
    /// A static class to map objects related to shopping cart items.
    /// </summary>
    public static class CartItemMapper
    {

        /// <summary>
        /// Maps a <see cref="CartItemDto"/> object to a <see cref="CartItem"/> object.
        /// </summary>
        /// <param name="cartItemDto">The <see cref="CartItemDto"/> object containing the data to map.</param>
        /// <returns>A new <see cref="CartItem"/> object with the mapped data.</returns>
        public static CartItem ToCartItem(this CartItemDto cartItemDto){

            return new CartItem
        {
            quantity = cartItemDto.quantity,
            productId = cartItemDto.productId
        };
        }
    }
}