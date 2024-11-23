using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taller1_WebMovil.Src.DTOs;
using Taller1_WebMovil.Src.Interface;
using Taller1_WebMovil.Src.Models;
using Taller1_WebMovil.Src.Services.Interfaces;

namespace Taller1_WebMovil.Src.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private const string CartCookieKey = "GuestCart";
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ICartItemRepository _cartItemRepository;

        public ShoppingCartController(IShoppingCartService shoppingCartService,ICartItemRepository cartItemRepository)
        {
            _shoppingCartService = shoppingCartService;
            _cartItemRepository = cartItemRepository;
        }

        [HttpGet("GetCartItems")]
        public async Task<IActionResult> GetCartItems()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Usuario registrado: obtener el carrito desde la base de datos
                var userId = GetUserId();
                var cartItems = await _shoppingCartService.GetCartItems(userId);
                return Ok(cartItems);
            }
            else
            {
                // Usuario no registrado: obtener el carrito desde las cookies
                var cart = GetCartFromCookies();
                return Ok(cart);
            }
        }

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart([FromBody] CartItemDto cartItemDto)
        {
            if (User.Identity.IsAuthenticated)
            {
                // Usuario registrado: agregar al carrito en la base de datos
                var userId = GetUserId();
                var cartItem = await _cartItemRepository.AddToCart(userId, cartItemDto);
            }
            else
            {
                // Usuario no registrado: manejar carrito en cookies
                var userGuid = GetOrCreateUserGuid();
                var cart = GetCartFromCookies();

                // Buscar si el producto ya está en el carrito
                var existingItem = cart.FirstOrDefault(c => c.productId == cartItemDto.productId);
                if (existingItem != null)
                {
                    // Si ya está, actualizar la cantidad
                    existingItem.quantity += cartItemDto.quantiy;
                }
                else
                {
                    // Si no está, agregar un nuevo ítem al carrito
                    var newCartItem = new CartItem
                    {
                        id = cart.Count > 0 ? cart.Max(c => c.id) + 1 : 1, // Asignar un ID único
                        productId = cartItemDto.productId,
                        quantity = cartItemDto.quantiy
                        
                    };

                    cart.Add(newCartItem);
                }

                // Guardar el carrito actualizado en las cookies
                SaveCartToCookies(userGuid, cart);
            }
            return Ok("Producto agregado al carrito");
        }

        [HttpDelete("DeleteCartItem/{id}")]
        public async Task<ActionResult> DeleteCartItem(int id)
        {
            // Comprobar si el usuario está autenticado
            if (User.Identity?.IsAuthenticated == true)
            {
                // El usuario está autenticado: trabajar con la base de datos
                var userId = GetUserId();
                var shoppingCart = _shoppingCartService.DeleteCartItem(userId,id);
            }
            else
            {
                // Usuario no autenticado: trabajar con cookies
                var userGuid = GetOrCreateUserGuid();

                // Obtener los items del carrito desde las cookies
                var cartItems = GetCartItemsFromCookies(userGuid);

                // Buscar el ticket a eliminar
                var cartItemToRemove = cartItems.FirstOrDefault(ci => ci.id == id);

                if (cartItemToRemove == null)
                {
                    return NotFound("El ticket no se encuentra en el carrito.");
                }

                // Eliminar el item del carrito
                cartItems.Remove(cartItemToRemove);

                // Guardar los cambios en las cookies
                SaveCartToCookies(userGuid, cartItems);
            }

            return NoContent();
        }


        private string GetUserId()
        {
            return User.Identity?.Name ?? throw new InvalidOperationException("El usuario no está autenticado");
        }
        
        private List<CartItem> GetCartFromCookies()
        {
            var cookieValue = Request.Cookies[CartCookieKey];
            return string.IsNullOrEmpty(cookieValue)
                ? new List<CartItem>()
                : JsonSerializer.Deserialize<List<CartItem>>(cookieValue) ?? new List<CartItem>();
        }
        private void SaveCartToCookies(string userGuid,List<CartItem> cart)
        {
            var serializedCart = JsonSerializer.Serialize(cart);
            var cookieOptions = new CookieOptions
            {
                Path = "/",
                HttpOnly = false,
                Secure = false,
                Expires = DateTime.Now.AddDays(7)
            };

            Response.Cookies.Append(CartCookieKey, serializedCart, cookieOptions);
        }

        /// <summary>
        /// Obtiene o crea un GUID para el usuario actual.
        /// </summary>
        /// <returns>
        /// El GUID del usuario.
        /// </returns>
        private string GetOrCreateUserGuid()
        {
            var userGuid = Request.Cookies[CartCookieKey];

            if (string.IsNullOrEmpty(userGuid))
            {
                userGuid = Guid.NewGuid().ToString();
                var cookieOptions = new CookieOptions
                {
                    Path = "/",
                    HttpOnly = false,
                    Secure = false,
                    Expires = DateTime.Now.AddDays(7)
                };
                Response.Cookies.Append(CartCookieKey, userGuid, cookieOptions);
            }

            return userGuid;
        }

        /// <summary>
        /// Obtiene los cartItems del usuario mediante su GUID.
        /// </summary>
        /// <param name="userGuid">
        /// El GUID del usuario.
        /// </param>
        /// <returns>
        /// La lista de cartItems del usuario.
        /// </returns>
        private List<CartItem> GetCartItemsFromCookies(string userGuid)
        {
            var cookieValue = Request.Cookies[CartCookieKey + "_" + userGuid];
            if (!string.IsNullOrEmpty(cookieValue))
            {
                return JsonSerializer.Deserialize<List<CartItem>>(cookieValue) ?? [];
            }
            return [];
        }

        
    }
}