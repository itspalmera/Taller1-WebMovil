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
        public async Task<IEnumerable<Purchase?>> SearchPurchase(int page, string name, int pageSize,bool sort)
        {
            int totalPurchase = await _context.Purchases.CountAsync();
            if (page < 1) page = 1;
            if (pageSize <2) pageSize =2;
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

        public async Task<IEnumerable<Purchase?>> ViewAllPurchase(int page, int pageSize,bool sort)
        {
            int totalPurchase = await _context.Purchases.CountAsync();
            if (page < 1) page = 1;
            if (pageSize <2) pageSize =2;
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
    }
}