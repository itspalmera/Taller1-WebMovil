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

    /// <summary>
    /// Controller for managing the user's shopping cart operations.
    /// </summary
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private const string CartCookieKey = "GuestCart";
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCartController"/> class.
        /// </summary>
        /// <param name="shoppingCartService">Service for handling shopping cart logic.</param>
        /// <param name="cartItemRepository">Repository for managing cart items.</param>
        /// <param name="productRepository">Repository for managing product data.</param>
        public ShoppingCartController(IShoppingCartService shoppingCartService, ICartItemRepository cartItemRepository, IProductRepository productRepository)
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
        [HttpPost("SyncCart")]
        public async Task<IActionResult> SyncCart()
        {
            // Verificar si el usuario está autenticado
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized("El usuario no está autenticado.");
            }

            // Obtener el ID del usuario
            var userId = GetUserId(); // Asegúrate de tener este método que obtiene el ID del usuario autenticado

            // Obtener el carrito de las cookies
            var cookieValue = Request.Cookies[CartCookieKey];
            if (string.IsNullOrWhiteSpace(cookieValue)) return Ok("No hay elementos en el carrito de las cookies.");

            // Deserializar los elementos del carrito desde las cookies
            var cookieCartItems = JsonSerializer.Deserialize<List<CartItem>>(cookieValue);
            if (cookieCartItems == null || !cookieCartItems.Any()) return Ok("El carrito de las cookies está vacío.");

            // Recorrer los productos del carrito en las cookies
            foreach (var cookieItem in cookieCartItems)
            {
                var product = await _productRepository.GetProductByIdAsync(cookieItem.productId);
                if (product == null)
                {
                    // Si el producto no existe en la base de datos, lo ignoramos
                    continue;
                }

                // Verificar si este producto ya existe en el carrito del usuario
                var existingItem = await _cartItemRepository.GetCartItemByUserAndProduct(userId, cookieItem.productId);

                if (existingItem != null)
                {
                    // Si el producto ya está en el carrito, actualizar la cantidad
                    existingItem.quantity += cookieItem.quantity;
                    await _cartItemRepository.UpdateCartItem(userId,existingItem); // Actualizar en la base de datos
                }
                else
                {
                    // Si el producto no existe en el carrito del usuario, agregarlo
                    var newCartItem = new CartItemDto
                    {
                        productId = cookieItem.productId,
                        quantity = cookieItem.quantity
                    };
                    await _cartItemRepository.AddToCart(userId, newCartItem);
                }
            }

            // Eliminar la cookie después de sincronizar
            Response.Cookies.Delete(CartCookieKey);

            // Devolver una respuesta indicando que la sincronización fue exitosa
            return Ok(new { message ="Carrito sincronizado con éxito."});
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
            if (cartItemDto.quantity < 1) return BadRequest("La cantidad no puede ser menor a 1.");
            if (User.Identity.IsAuthenticated)
            {
                // Usuario registrado: agregar al carrito en la base de datos
                var userId = GetUserId();

                // Sincronizar el carrito de cookies con la base de datos (si aplica)
                var cookieValue = Request.Cookies[CartCookieKey];
                if (!string.IsNullOrWhiteSpace(cookieValue))
                {
                    var cookieCartItems = JsonSerializer.Deserialize<List<CartItem>>(cookieValue);
                    if (cookieCartItems != null)
                    {
                        foreach (var cookieItem in cookieCartItems)
                        {
                            await _cartItemRepository.AddToCart(userId, new CartItemDto
                            {
                                productId = cookieItem.productId,
                                quantity = cookieItem.quantity
                            });
                        }
                        Response.Cookies.Delete(CartCookieKey); // Eliminar la cookie después de sincronizar
                    }
                }

                // Agregar el producto actual al carrito de la base de datos
                var cartItem = await _cartItemRepository.AddToCart(userId, cartItemDto);
                if (!cartItem) return NotFound("El producto ingresado no existe.");

                return Ok(cartItemDto.quantity > 0 ? new { message = "Producto agregado al carrito" } : new { message = "Producto eliminado del carrito" } );
            }
            else
            {
                // Usuario no registrado: agregar al carrito en las cookies
                var cookieValue = Request.Cookies[CartCookieKey];

                // Obtener o inicializar el carrito
                var cartItems = string.IsNullOrWhiteSpace(cookieValue)
                    ? new List<CartItem>()
                    : JsonSerializer.Deserialize<List<CartItem>>(cookieValue) ?? new List<CartItem>();

                // Buscar el producto en el carrito
                var existingItem = cartItems.FirstOrDefault(item => item.productId == cartItemDto.productId);

                if (existingItem != null)
                {
                    existingItem.quantity += cartItemDto.quantity;

                    // Eliminar el producto si la cantidad es menor a 1
                    if (existingItem.quantity < 1) cartItems.Remove(existingItem);
                }
                else
                {
                    // Agregar un nuevo producto al carrito si no existe y la cantidad es válida
                    if (cartItemDto.quantity > 0)
                    {
                        var product = await _productRepository.GetProductByIdAsync(cartItemDto.productId);
                        if (product == null) return NotFound("El producto ingresado no existe.");

                        cartItems.Add(new CartItem
                        {
                            productId = cartItemDto.productId,
                            Product = product,
                            quantity = cartItemDto.quantity
                        });
                    }
                    else
                    {
                        return BadRequest("La cantidad no puede ser menor a 1 para un nuevo producto.");
                    }
                }

                // Guardar el carrito actualizado en las cookies
                SaveCartItemsToCookies(GetOrCreateUserGuid(), cartItems);

                return Ok(cartItemDto.quantity > 0 ? new { message = "Producto agregado al carrito" } : new { message = "Producto eliminado del carrito" });
            }
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
                var shoppingCart = await _cartItemRepository.DeleteCartItem(userId, idProduct);
                if (!shoppingCart) return BadRequest("No se pudo eliminar el producto, ya que no se encuentra en su carrito.");

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
            // Obtén el valor de la cookie
            // Obtener el valor de la cookie
            var cookieValue = Request.Cookies[CartCookieKey];

            // Validar si la cookie no existe o está vacía
            if (string.IsNullOrWhiteSpace(cookieValue))
            {
                return new List<ShoppingCartInfoDto>(); // Devuelve un carrito vacío si no hay cookie
            }

            try
            {
                // Intentar deserializar el JSON
                var cartItems = JsonSerializer.Deserialize<List<CartItem>>(cookieValue);

                // Si el carrito deserializado es nulo, inicializar una lista vacía
                if (cartItems == null)
                {
                    return new List<ShoppingCartInfoDto>();
                }

                // Mapear los CartItems a ShoppingCartInfoDto
                var cartItemsDto = cartItems
                    .Select(cartItem => ShoppingCartMapper.ToShoppingCartInfoDto(cartItem))
                    .ToList();

                return cartItemsDto;
            }
            catch (JsonException ex)
            {
                // Manejar errores de deserialización
                Console.WriteLine($"Error al deserializar la cookie: {ex.Message}");
                return new List<ShoppingCartInfoDto>(); // Devuelve un carrito vacío si el JSON es inválido
            }

        }
        /// <summary>
        /// Saves the shopping cart items to the cookies.
        /// </summary>
        /// <param name="userGuid">The user's GUID.</param>
        /// <param name="cart">The list of cart items to save.</param>
        private void SaveCartItemsToCookies(string userGuid, List<CartItem> cart)
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
            return new List<CartItem>(); ;
        }


    }
}