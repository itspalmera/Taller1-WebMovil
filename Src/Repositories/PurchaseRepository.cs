using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Taller1_WebMovil.Src.Data;
using Taller1_WebMovil.Src.DTOs.Purchase;
using Taller1_WebMovil.Src.Interface;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ApplicationDbContext _context;

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
        public async Task<IEnumerable<Purchase?>> SearchPurchase(int page, string name, int pageSize,bool sort)
        {
            int totalPurchase = await _context.Purchases.CountAsync();
            if (page < 1) page = 1;
            if (pageSize <10) pageSize =10;
            int maxPage = (int)Math.Ceiling((Double)totalPurchase/page);
            if (page>maxPage) page = maxPage;


            var purchases = await _context.Purchases.Where(p=> p.user.name.ToLower().Contains(name.ToLower()))
                                            .Include(p => p.purchaseReceipt)
                                            .Include(p => p.user)
                                            .Skip((page - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToListAsync();
            if(sort) purchases.OrderByDescending(p => p.purchaseReceipt.purchaseDate);
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
        public async Task<IEnumerable<Purchase?>> ViewAllPurchase(int page, int pageSize,bool sort)
        {
            int totalPurchase = await _context.Purchases.CountAsync();
            if (page < 1) page = 1;
            if (pageSize <10) pageSize =10;
            int maxPage = (int)Math.Ceiling((Double)totalPurchase/page);
            if (page>maxPage) page = maxPage;


            var purchases = await _context.Purchases.Include(p => p.purchaseReceipt)
                                            .Include(p => p.user)
                                            .Skip((page - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToListAsync();
            if(sort) purchases.OrderByDescending(p => p.purchaseReceipt.purchaseDate);
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
         public async Task<IEnumerable<Purchase?>> ViewAllPurchaseClient(int page, int pageSize,User user){

            var purchases = await _context.Purchases.Where(p=> p.user.Id == user.Id)
                                            .Include(p => p.purchaseReceipt)
                                            .Include(p => p.user)
                                            .Include(p => p.product)
                                            .OrderByDescending(p => p.purchaseReceipt.purchaseDate)
                                            .ToListAsync();
            return purchases;
         }
    }
}