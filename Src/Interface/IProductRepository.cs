using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Interface
{
    public interface IProductRepository
    {
        Task<bool> ExistsByName(string name);
        Task<Product> AddProductAsync(Product product);
        Task<Product?> GetProductByIdAsync(int id);
        Task<List<Product>> GetAllProductsAsync(string? text, string? category, string? sort);
        Task DeleteProductAsync(Product product);
        Task UpdateProductAsync(Product product);
    }
}