using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taller1_WebMovil.Src.DTOs;
using Taller1_WebMovil.Src.DTOs.ShoppingCart;
using Taller1_WebMovil.Src.Interface;
using Taller1_WebMovil.Src.Mapper;
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
        private readonly IProductRepository _productRepository;

        public ShoppingCartController(IShoppingCartService shoppingCartService,ICartItemRepository cartItemRepository,IProductRepository productRepository)
        {
            _shoppingCartService = shoppingCartService;
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
        }
        /// <summary>
        /// Retrieves the items in the user's shopping cart.
        /// </summary>
        /// <returns>The list of items in the cart.</returns>
        /// <response code="200">Returns the items in the cart.</response>
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
        /// <summary>
        /// Adds an item to the user's shopping cart.
        /// </summary>
        /// <param name="cartItemDto">The item to be added to the cart.</param>
        /// <returns>A message indicating the result of the operation.</returns>
        /// <response code="200">Returns a success message.</response>
        /// <response code="400">If the quantity is invalid or the product does not exist.</response>
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart([FromBody] CartItemDto cartItemDto)
        {
            if (User.Identity.IsAuthenticated)
            {
                // Usuario registrado: agregar al carrito en la base de datos
                var userId = GetUserId();
                //Crear metodo para poder ver si hay alguna cookie con informacion, si hay, se actualiza el carrito y se elimina la cookie
                //si no, no hacer nada.

                var cartItem = await _cartItemRepository.AddToCart(userId, cartItemDto);
                if(!cartItem) return NotFound("El producto ingresado no existe.");
            }
            else
            {
                // Usuario no registrado: agregar a las cookies
                var cookieValue = Request.Cookies[CartCookieKey];
                // Obtener o crear un GUID para el usuario no autenticado
                var userGuid = GetOrCreateUserGuid();

                // Obtener los productos del carrito desde las cookies
                var cartItems = JsonSerializer.Deserialize<List<CartItem>>(cookieValue) ?? new List<CartItem>();

                // Verificar si el producto ya existe en el carrito
                var existingItem = cartItems.FirstOrDefault(item => item.productId == cartItemDto.productId);

                if (existingItem != null)
                {
                    if(existingItem.quantity+cartItemDto.quantity <1)
                    // Eliminar el item del carrito si la cantidad es menor que 1
                    cartItems.Remove(existingItem);
                    else{
                    existingItem.quantity += cartItemDto.quantity;
                    }   
                }
                else
                {
                    if(cartItemDto.quantity<1)return BadRequest("La cantidad no puede ser menor a 1.");
                    // Si el producto no está en el carrito, agregarlo como un nuevo item
                    var newCartItem = new CartItem
                    {
                        productId = cartItemDto.productId,
                        Product = await _productRepository.GetProductByIdAsync(cartItemDto.productId),
                        quantity = cartItemDto.quantity
                    };
                    string nameProduct = newCartItem.Product.name;

                    cartItems.Add(newCartItem);
                }
                // Guardar el carrito actualizado en las cookies
                SaveCartItemsToCookies(userGuid, cartItems);
            }
            if(cartItemDto.quantity<0){
                return Ok($"Se eliminaron ${-1*cartItemDto.quantity} del carrito");
            }else{return Ok("Producto agregado al carrito");}
        }
        /// <summary>
        /// Removes an item from the user's shopping cart.
        /// </summary>
        /// <param name="idProduct">The ID of the product to be removed.</param>
        /// <returns>A message indicating the result of the operation.</returns>
        /// <response code="200">Returns a success message.</response>
        /// <response code="400">If the item does not exist in the cart.</response>
        [HttpDelete("DeleteCartItem/{idProduct}")]
        public async Task<ActionResult> DeleteCartItem(int idProduct)
        {
            // Comprobar si el usuario está autenticado
            if (User.Identity?.IsAuthenticated == true)
            {
                // El usuario está autenticado: trabajar con la base de datos
                var userId = GetUserId();
                var shoppingCart =await _cartItemRepository.DeleteCartItem(userId,idProduct);
                if(!shoppingCart)return BadRequest("No se pudo eliminar el producto, ya que no se encuentra en su carrito.");
                
            }
            else
            {
               // Usuario no registrado: agregar a las cookies
                var cookieValue = Request.Cookies[CartCookieKey];
                // Obtener o crear un GUID para el usuario no autenticado
                var userGuid = GetOrCreateUserGuid();

                // Obtener los productos del carrito desde las cookies
                var cartItems = JsonSerializer.Deserialize<List<CartItem>>(cookieValue) ?? new List<CartItem>(); 
                // Verificar si el producto ya existe en el carrito
                var existingItem = cartItems.FirstOrDefault(item => item.productId == idProduct);

                if (existingItem == null)
                {
                    return BadRequest("No se pudo eliminar el producto, ya que no se encuentra en su carrito.");
                }
                cartItems.Remove(existingItem);
                // Guardar el carrito actualizado en las cookies
                SaveCartItemsToCookies(userGuid, cartItems);
            }

            return Ok("Se eliminó el producto.");
        }

        /// <summary>
        /// Retrieves the authenticated user's ID.
        /// </summary>
        /// <returns>The user's ID.</returns>
        private string GetUserId()
        {
            return User.Identity?.Name ?? throw new InvalidOperationException("El usuario no está autenticado");
        }
        /// <summary>
        /// Retrieves the shopping cart items from the cookies.
        /// </summary>
        /// <returns>A list of shopping cart items.</returns>
        private List<ShoppingCartInfoDto> GetCartFromCookies()
        {
            var cookieValue = Request.Cookies[CartCookieKey];
            // Deserializar el carrito de las cookies
            var cartItems = JsonSerializer.Deserialize<List<CartItem>>(cookieValue) ?? new List<CartItem>();

            // Mapear los CartItems a ShoppingCartInfoDto
            var cartItemsDto = cartItems.Select(cartItem => ShoppingCartMapper.ToShoppingCartInfoDto(cartItem)).ToList();
            return cartItemsDto;
        }
        /// <summary>
        /// Saves the shopping cart items to the cookies.
        /// </summary>
        /// <param name="userGuid">The user's GUID.</param>
        /// <param name="cart">The list of cart items to save.</param>
        private void SaveCartItemsToCookies(string userGuid,List<CartItem> cart)
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
        /// Retrieves or creates a GUID for the user.
        /// </summary>
        /// <returns>The user's GUID.</returns>
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
        /// Retrieves the user's shopping cart items from cookies using their GUID.
        /// </summary>
        /// <param name="userGuid">The unique identifier (GUID) of the user.</param>
        /// <returns>A list of shopping cart items (<see cref="CartItem"/>). Returns an empty list if no items are found.</returns>
        private List<CartItem> GetCartItemsFromCookies(string userGuid)
        {
            var cookieValue = Request.Cookies[CartCookieKey + "_" + userGuid];
            if (!string.IsNullOrEmpty(cookieValue))
            {
                return JsonSerializer.Deserialize<List<CartItem>>(cookieValue) ?? new List<CartItem>();
            }
            return new List<CartItem>();;
        }

        
    }
}