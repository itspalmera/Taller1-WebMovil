using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Taller1_WebMovil.Src.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Taller1_WebMovil.Src.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    
    }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole {
                    Id="1",Name="Administrador",NormalizedName="ADMINISTRADOR"
                },
                new IdentityRole {
                    Id="2",Name="Cliente",NormalizedName="CLIENTE"
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
    
        }
        public DbSet<Product> Products {get; set;} = null!;
        public DbSet<Purchase> Purchases {get; set;} = null!;
        public DbSet<Gender> Genders {get; set;} = null!;
        public DbSet<Category> Categories {get; set;} = null!;
        public DbSet<PurchaseReceipt> PurchaseReceipts {get; set;} = null!;
        public DbSet<ShoppingCart> ShoppingCarts { get; set; } = null!;
        public DbSet<CartItem> CartItems { get; set; } = null!;
    }
}