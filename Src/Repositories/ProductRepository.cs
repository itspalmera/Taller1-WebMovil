using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taller1_WebMovil.src.Interface;
using Taller1_WebMovil.Src.Data;
using Taller1_WebMovil.src.Models;



namespace Taller1_WebMovil.src.Repositories
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
    }
}