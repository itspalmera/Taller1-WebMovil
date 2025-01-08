using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Taller1_WebMovil.Src.Data;
using Taller1_WebMovil.Src.DTOs.Products;
using Taller1_WebMovil.Src.DTOs.Purchase;
using Taller1_WebMovil.Src.Interface;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Repositories
{
    /// <summary>
    /// Repository for managing purchase-related operations.
    /// </summary>
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PurchaseRepository"/> class.
        /// </summary>
        /// <param name="context">The application's database context.</param>
         public PurchaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Searches for purchases based on the user's name, with pagination and optional sorting by purchase date.
        /// </summary>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="name">The name of the user to search for in purchases.</param>
        /// <param name="pageSize">The number of purchases to display per page.</param>
        /// <param name="sort">If true, sorts the results by purchase date in descending order.</param>
        /// <returns>
        /// A list of purchases that match the search criteria, with pagination and optional sorting.
        /// </returns>
        /// <response code="200">Returns a list of purchases matching the search criteria.</response>
        /// <response code="400">Returns an empty list if no purchases are found or if invalid parameters are provided.</response>
        public async Task<IEnumerable<Purchase?>> SearchPurchase(int page, string name, int pageSize, bool sort)
        {
            int totalPurchase = await _context.Purchases.CountAsync();
            if (page < 1) page = 1;
            if (pageSize < 10) pageSize = 10;
            int maxPage = (int)Math.Ceiling((Double)totalPurchase / page);
            if (page > maxPage) page = maxPage;


            var purchases = await _context.Purchases.Where(p => p.user.name.ToLower().Contains(name.ToLower()))
                                            .Include(p => p.purchaseReceipt)
                                            .Include(p => p.user)
                                            .Skip((page - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToListAsync();
            if (sort) purchases.OrderByDescending(p => p.purchaseReceipt.purchaseDate);
            return purchases;
        }

        /// <summary>
        /// Retrieves all purchases with pagination and optional sorting by purchase date.
        /// </summary>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="pageSize">The number of purchases to display per page.</param>
        /// <param name="sort">If true, sorts the results by purchase date in descending order.</param>
        /// <returns>
        /// A list of all purchases, with pagination and optional sorting.
        /// </returns>
        /// <response code="200">Returns a list of all purchases.</response>
        /// <response code="400">Returns an empty list if invalid parameters are provided.</response>
        public async Task<IEnumerable<Purchase?>> ViewAllPurchase(int page, int pageSize, bool sort)
        {
            int totalPurchase = await _context.Purchases.CountAsync();
            if (page < 1) page = 1;
            if (pageSize < 10) pageSize = 10;
            int maxPage = (int)Math.Ceiling((Double)totalPurchase / page);
            if (page > maxPage) page = maxPage;


            var purchases = await _context.Purchases.Include(p => p.purchaseReceipt)
                                            .Include(p => p.user)
                                            .GroupBy(p => p.purchaseReceiptId)
                                            .Select(p => p.FirstOrDefault()) 
                                            .Skip((page - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToListAsync();
            if (sort) purchases.OrderByDescending(p => p.purchaseReceipt.purchaseDate);
            return purchases;
        }

        /// <summary>
        /// Retrieves all purchases made by a specific user, sorted by purchase date in descending order.
        /// </summary>
        /// <param name="page">The page number for pagination (not used in this method, but could be added for consistency).</param>
        /// <param name="pageSize">The number of purchases to display per page (not used in this method, but could be added for consistency).</param>
        /// <param name="user">The user whose purchases are to be retrieved.</param>
        /// <returns>
        /// A list of purchases made by the specified user, sorted by purchase date in descending order.
        /// </returns>
        /// <response code="200">Returns a list of the user's purchases.</response>
        /// <response code="400">Returns an empty list if no purchases are found for the user.</response>
        public async Task<IEnumerable<Purchase>> ViewAllPurchaseClient(int page, int pageSize, User user)
        {
            int totalPurchase = await _context.Purchases.CountAsync();
            if (page < 1) page = 1;
            if (pageSize < 10) pageSize = 10;
            int maxPage = (int)Math.Ceiling((Double)totalPurchase / page);
            if (page > maxPage) page = maxPage;


            var purchases = await _context.Purchases.Where(p => p.user.Id == user.Id)
                                            .Include(p => p.purchaseReceipt)
                                            .Include(p => p.user)
                                            .GroupBy(p => p.purchaseReceiptId)
                                            .Select(p => p.FirstOrDefault()) 
                                            .Skip((page - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToListAsync();
            return purchases;
        }
        /// <summary>
        /// Processes a new purchase by creating a purchase receipt and adding the purchase details to the database.
        /// The method also clears the user's shopping cart after the purchase is successfully processed.
        /// </summary>
        /// <param name="newPurchaseDto">The data transfer object containing the details of the new purchase, such as the shipping information.</param>
        /// <param name="user">The user who is making the purchase.</param>
        /// <returns>
        /// A boolean indicating whether the purchase process was successful. Returns <c>true</c> if the purchase was processed and the cart items were removed, otherwise <c>false</c>.
        /// </returns>
        /// <response code="200">Returns <c>true</c> if the purchase was successfully processed and the cart items were cleared.</response>
        /// <response code="400">Returns <c>false</c> if the shopping cart is empty or there is an error during the purchase process.</response>
        public async Task<bool> ProcessPurchase(NewPurchaseDto newPurchaseDto, User user)
        {
            ShoppingCart shoppingCart = await _context.ShoppingCarts.Where(sc => sc.userId == user.Id)
                                                                    .Include(cart => cart.Items)
                                                                    .FirstOrDefaultAsync();
            if (shoppingCart == null) return false;
            int total = shoppingCart.Items.Sum(item => item.quantity * item.price);

            PurchaseReceipt purchaseReceipt = new PurchaseReceipt
            {
                country = newPurchaseDto.country,
                city = newPurchaseDto.city,
                district = newPurchaseDto.district,
                street = newPurchaseDto.street,
                purchaseDate = DateOnly.FromDateTime(DateTime.Now),
                totalPrice = total
            };
            // Agregar la boleta a la base de datos
            await _context.PurchaseReceipts.AddAsync(purchaseReceipt);
            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();
            // Crear las compras para cada ítem en el carrito
            foreach (var item in shoppingCart.Items)
            {
                var purchase = new Purchase
                {
                    quantity = item.quantity,
                    totalPrice = item.quantity * item.price,
                    productId = item.productId,
                    userId = user.Id,
                    purchaseReceiptId = purchaseReceipt.id,
                    product = item.Product,
                    user = shoppingCart.user,
                    purchaseReceipt = purchaseReceipt
                };

                // Agregar las compras a la base de datos
                await _context.Purchases.AddAsync(purchase);
                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
            }

            if (shoppingCart != null)
            {
                // Eliminar los ítems del carrito
                _context.CartItems.RemoveRange(shoppingCart.Items);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<PurchaseInfoClientDto> GetPurchaseById(int id)
        {
            var purchases = await _context.Purchases.Where(p => p.purchaseReceiptId == id)
                                                    .Include(p => p.purchaseReceipt)
                                                    .Include(p => p.product)
                                                    .ThenInclude(product => product.category)
                                                    .ToListAsync();

            if (purchases == null || purchases.Count == 0)
                return null;

            var result = new PurchaseInfoClientDto
            {
                purchaseDate = purchases.First().purchaseReceipt.purchaseDate.ToString("dd/MM/yyyy"),
                totalPurchasePrice = purchases.First().purchaseReceipt.totalPrice,
                ProductDetails = purchases.Select(p => new ProductInfoClientDto
                {
                    nameProduct = p.product.name,
                    Type = p.product.category.name,
                    price = p.product.price.ToString(),
                    quantity = p.quantity.ToString(),
                    totalPrice = (p.product.price * p.quantity).ToString()
                }).ToList()
            };

            return result;
        }
    }
}