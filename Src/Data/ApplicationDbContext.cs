using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
        
    }
        public DbSet<Product> Products {get; set;} = null!;
        public DbSet<Purchase> Purchases {get; set;} = null!;
        public DbSet<Gender> Genders {get; set;} = null!;
        public DbSet<Category> Categories {get; set;} = null!;
        public DbSet<PurchaseReceipt> PurchaseReceipts {get; set;} = null!;
    }
}