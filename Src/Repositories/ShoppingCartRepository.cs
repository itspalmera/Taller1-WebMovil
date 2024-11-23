using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller1_WebMovil.Src.Data;
using Taller1_WebMovil.Src.Interface;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShoppingCart?>> GetCartItems(string userId)
        {
            var cartItems = await _context.ShoppingCarts.Where(sc => sc.userId == userId)
                    .Include(sc => sc.cartItem)
                    .ToListAsync();
            return cartItems;
        }
        public async Task<CartItem?> GetCartItemByProduct(string userId,int productId){
            var cartItems = await _context.ShoppingCarts.Where(sc => sc.userId == userId)
                                                        .Select(sc => sc.cartItem)
                                                        .ToListAsync();

            // Buscar entre los CartItems el que tenga el productId especificado
            var cartItem = cartItems.FirstOrDefault(ci => ci.productId == productId);
            
            return cartItem;
        }

        public async Task<bool> DeleteCartItem(string userId, int productId)
        {
            // Buscar el carrito asociado al usuario autenticado
                var shoppingCart = await _context.ShoppingCarts
                    .Include(sc => sc.cartItem)
                    .FirstOrDefaultAsync(sc => sc.userId == userId);

                if (shoppingCart == null)
                {
                    return false;
                }
                var cartItem = await _context.CartItems.Where(ci => ci.productId ==productId).FirstOrDefaultAsync();
                

                // Eliminar el item del carrito
                _context.ShoppingCarts.Remove(shoppingCart);
                await _context.SaveChangesAsync();
                return true;
        }
    }
}