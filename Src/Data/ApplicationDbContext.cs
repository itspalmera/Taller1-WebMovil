using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Taller1_WebMovil.src.Models;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Data
{
    public class ApplicationDbContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
    {
        public DbSet<User> Users {get; set;} = null!;
        public DbSet<Product> Products {get; set;} = null!;
        public DbSet<Purchase> Purchases {get; set;} = null!;
        public DbSet<Role> Roles {get; set;} = null!;
        public DbSet<Gender> Genders {get; set;} = null!;
        public DbSet<Category> Categories {get; set;} = null!;
        public DbSet<PurchaseReceipt> PurchaseReceipts {get; set;} = null!;
    }
}