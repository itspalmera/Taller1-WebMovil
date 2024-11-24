using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Interface
{
    public interface IProductRepository
    {
        /// <summary>
        /// Checks if a product with the specified name exists in the system.
        /// </summary>
        /// <param name="name">The name of the product.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the product exists.</returns>
        Task<bool> ExistsByName(string name);

        /// <summary>
        /// Adds a new product to the system.
        /// </summary>
        /// <param name="product">The product to be added.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added product.</returns>
        Task<Product> AddProductAsync(Product product);

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the product or null if not found.</returns>
        Task<Product?> GetProductByIdAsync(int id);

        /// <summary>
        /// Retrieves all products, with optional filters for name, category, and sorting.
        /// </summary>
        /// <param name="text">Optional text filter to search for products by name or description.</param>
        /// <param name="category">Optional category filter to narrow down the search.</param>
        /// <param name="sort">Optional sorting criteria for the product list.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of products that match the criteria.</returns>
        Task<List<Product>> GetAllProductsAsync(string? text, string? category, string? sort);

        /// <summary>
        /// Deletes a product from the system.
        /// </summary>
        /// <param name="product">The product to be deleted.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteProductAsync(Product product);

        /// <summary>
        /// Updates an existing product in the system.
        /// </summary>
        /// <param name="product">The product with updated details.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateProductAsync(Product product);
    }
}