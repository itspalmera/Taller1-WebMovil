using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Interface
{
    public interface IProductRepository
    {
        Task<bool> ExistsByCode(string code);
        Task<Product> AddProductAsync(Product product);
        //Task<Product?> GetUserByIdAsync(int id);
        Task<List<Product>> GetAllProductsAsync(string? category);
        Task DeleteProductAsync(Product product);
        Task UpdateProductAsync(Product product);
    }
}