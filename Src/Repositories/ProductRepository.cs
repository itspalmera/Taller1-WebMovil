using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taller1_WebMovil.Src.Interface;
using Taller1_WebMovil.Src.Data;
using Taller1_WebMovil.Src.Models;
using Microsoft.EntityFrameworkCore;
using Taller1_WebMovil.Src.DTOs.Products;



namespace Taller1_WebMovil.Src.Repositories
{
    /// <summary>
    /// Repository implementation for managing product operations in the database.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="datacontext">The application's database context.</param>
        public ProductRepository(ApplicationDbContext datacontext)
        {
            _dataContext = datacontext;
        }


        /// <summary>
        /// Adds a new product to the database.
        /// </summary>
        /// <param name="product">The product to add.</param>
        /// <returns>The added product.</returns>
        public async Task<Product> AddProductAsync(Product product)
        {
            _dataContext.Products.Add(product);
            await _dataContext.SaveChangesAsync();
            return product;
        }


        /// <summary>
        /// Deletes a product from the database.
        /// </summary>
        /// <param name="product">The product to delete.</param>
        public async Task DeleteProductAsync(Product product)
        {
            _dataContext.Products.Remove(product);
            await _dataContext.SaveChangesAsync();
        }


        /// <summary>
        /// Updates an existing product in the database.
        /// </summary>
        /// <param name="product">The product to update.</param>
        public async Task UpdateProductAsync(Product product)
        {
            _dataContext.Products.Update(product);
            await _dataContext.SaveChangesAsync();
        }
        public async Task<bool> existProduct(string name, int id)
        {
            return await _dataContext.Products.AnyAsync(x => x.name == name && x.id != id);
        }


        /// <summary>
        /// Checks if a product exists in the database by its name.
        /// </summary>
        /// <param name="name">The name of the product to check.</param>
        /// <returns>True if the product exists; otherwise, false.</returns>
        public async Task<bool> ExistsByName(string name)
        {
            return await _dataContext.Products.AnyAsync(x => x.name == name);
        }


        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="id">The product's identifier.</param>
        /// <returns>The product, or null if it does not exist.</returns>
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _dataContext.Products.FindAsync(id);
        }


        /// <summary>
        /// Retrieves all products with optional filters for text, category, and sorting.
        /// </summary>
        /// <param name="text">Text to filter products by name.</param>
        /// <param name="category">Category to filter products.</param>
        /// <param name="sort">Sort order ("asc" for ascending, "desc" for descending) by price.</param>
        /// <returns>A list of filtered and sorted products.</returns>
        public async Task<List<ProductDto>> GetAllProductsAsync(int page, string? text, string? category, string? sort)
        {
            IQueryable<Product> query = _dataContext.Products.AsQueryable().Include(p => p.category);

            // Filtrar por texto
            if (!string.IsNullOrEmpty(text))
            {
                query = query.Where(x => x.name.Contains(text));
            }

            // Filtrar por categoría
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(x => x.category.name == category);
            }

            // Ordenar por precio
            if (!string.IsNullOrEmpty(sort))
            {
                query = sort.ToLower() == "asc"
                    ? query.OrderBy(x => x.price)
                    : query.OrderByDescending(x => x.price);
            }

            // Configuración de paginación
            int pageSize = 10;
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            // Proyección de los productos al DTO
            var products = await query.Select(product => new ProductDto
            {
                id = product.id.ToString(),
                name = product.name,
                type = product.category.name,
                price = product.price.ToString(),
                stock = product.stock.ToString(),
                image = product.image,
                enabled = product.enabled ? "Habilitado" : "Deshabilitado",
            }).ToListAsync();

            return products;
        }
        public async Task<bool> DeleteProduct(int id)
        {
            var deleteProduct = await _dataContext.Products.FindAsync(id);
            if(deleteProduct == null)
            {
                return false;
            }
            _dataContext.Products.Remove(deleteProduct);
            await _dataContext.SaveChangesAsync();
            return true;
        }
    }
}