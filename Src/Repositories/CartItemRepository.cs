using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Taller1_WebMovil.Src.Data;
using Taller1_WebMovil.Src.DTOs;
using Taller1_WebMovil.Src.DTOs.ShoppingCart;
using Taller1_WebMovil.Src.Interface;
using Taller1_WebMovil.Src.Mapper;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly ApplicationDbContext _context;
         public CartItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddToCart(string userId,CartItemDto cartItemDto)
        {

            // Convertir el CartItemDto en CartItem
            var cartItem = cartItemDto.ToCartItem();

            // Buscar el producto en la base de datos
            Product product = await _context.Products.Where(p => p.id == cartItem.productId).FirstOrDefaultAsync();
            if (product == null) return false; // Producto no encontrado

            // Asignar el producto al CartItem
            cartItem.product = product;

            // Verificar si ya existe un carrito para el usuario
            var shoppingCart = await _context.ShoppingCarts
                .Include(sc => sc.cartItem)
                .FirstOrDefaultAsync(sc => sc.userId == userId);

            if (shoppingCart != null)
            {
                // El carrito ya existe, verificar si el producto ya está en el carrito
                var existingCartItem = await _context.CartItems
                    .FirstOrDefaultAsync(ci => ci.productId == cartItem.productId && ci.id == shoppingCart.cartItemId);

                if (existingCartItem != null)
                {
                    // El producto ya está en el carrito, actualizar la cantidad
                    existingCartItem.quantity += cartItem.quantity; // Puedes ajustar cómo manejar la cantidad (sumar, reemplazar, etc.)
                    _context.CartItems.Update(existingCartItem);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            else
            {
                // No existe carrito para el usuario, crear uno nuevo
                shoppingCart = new ShoppingCart
                {
                    userId = userId,
                    cartItemId = cartItem.id
                };

                // Añadir el CartItem al contexto
                var entry = await _context.CartItems.AddAsync(cartItem);
                await _context.SaveChangesAsync();

                // Asociar el CartItem al ShoppingCart y agregarlo
                shoppingCart.cartItem = entry.Entity;

                User user = await _context.Users.Where(u => u.Id ==userId).FirstOrDefaultAsync();
                shoppingCart.user=user;
                await _context.ShoppingCarts.AddAsync(shoppingCart);
                await _context.SaveChangesAsync();
            }

            return true;
        }
    }
}