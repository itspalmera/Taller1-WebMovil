using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taller1_WebMovil.Src.Interface;
using Taller1_WebMovil.Src.Data;
using Taller1_WebMovil.Src.Models;
using Microsoft.EntityFrameworkCore;



namespace Taller1_WebMovil.Src.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dataContext;

        public ProductRepository(ApplicationDbContext datacontext)
        {
            _dataContext = datacontext;
        }


        //Add product to the database
        public async Task<Product> AddProductAsync(Product product)
        {
            _dataContext.Products.Add(product);
            await _dataContext.SaveChangesAsync();
            return product;
        }


        //Delete product from the database
        public async Task DeleteProductAsync(Product product)
        {
            _dataContext.Products.Remove(product);
            await _dataContext.SaveChangesAsync();
        }


        //Update product in the database
        public async Task UpdateProductAsync(Product product)
        {
            _dataContext.Products.Update(product);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsByName(string name)
        {
            return await _dataContext.Products.AnyAsync(x => x.name == name);
        }


        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _dataContext.Products.FindAsync(id);
        }


        public async Task<List<Product>> GetAllProductsAsync(string? text, string? category, string? sort)
        {
            var query = _dataContext.Products.AsQueryable();

            //Filter by text
            if(!string.IsNullOrEmpty(text))
            {
                query = query.Where(x => x.name.Contains(text));
            }

            //Filter by category
            if(!string.IsNullOrEmpty(category))
            {
                query = query.Where(x => x.category.name == category);
            }

            //Filter by sort
            if (!string.IsNullOrEmpty(sort))
            {
                query = sort.ToLower() == "asc" ? query.OrderBy(x => x.price) : query.OrderByDescending(x => x.price);
            }

            
            return await query.ToListAsync();
        }
    }
}