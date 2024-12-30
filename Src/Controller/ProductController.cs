using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taller1_WebMovil.Src.Interface;
using Taller1_WebMovil.Src.Models;
using Taller1_WebMovil.Src.DTOs.Products;
using Microsoft.EntityFrameworkCore;


namespace Taller1_WebMovil.Src.Controller
{

     /// <summary>
    /// Controller for managing product operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="productRepository">Injected product repository for data management.</param>
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="createProductDto">DTO containing product details to be created.</param>
        /// <returns>An HTTP response indicating the result of the operation.</returns>
        //TODO: Add product
        [HttpPost("")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            // Validar si la categoría existe
            var category = await _productRepository.GetCategoryByNameAsync(createProductDto.categoryName);
            if (category == null)
            {
                return BadRequest(new { message = "La categoría especificada no existe." });
            }

            // Crear el producto
            var newProduct = new Product
            {
                name = createProductDto.name,
                price = createProductDto.price,
                stock = createProductDto.stock,
                image = createProductDto.image,
                enabled = true,
                categoryName = category.name // Usar el nombre en la vista pero el ID internamente
            };

            await _productRepository.AddProductAsync(newProduct);
            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.id }, newProduct);
        }



        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="id">The product's identifier.</param>
        /// <returns>The requested product or a not found error if it does not exist.</returns>
        //TODO: Get por id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        /// <summary>
        /// Retrieves all products with optional filters.
        /// </summary>
        /// <param name="text">Text to search within product names.</param>
        /// <param name="category">Category filter for the products.</param>
        /// <param name="sort">Sorting criteria for the products.</param>
        /// <returns>A list of filtered and sorted products.</returns>
        //TODO: Get all products
        [HttpGet("")]
        public async Task<IActionResult> GetAllProducts([FromQuery] string? text,[FromQuery] string? category, [FromQuery] string? sort)
        {
            //var products = await _productRepository.GetAllProductsAsync(category);

            var products = await _productRepository.GetAllProductsAsync(text, category, sort);
            return Ok(products);
        }
        

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">The identifier of the product to be updated.</param>
        /// <param name="updateProductDto">DTO containing updated product details.</param>
        /// <returns>An HTTP response indicating the result of the operation.</returns>
        //TODO: Update product
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto updateProductDto)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if(product == null)
            {
                return NotFound(new {message = "El producto no existe"});
            }

            //Update the product
            product.name = updateProductDto.name;
            product.price = updateProductDto.price;
            product.stock = updateProductDto.stock;
            product.image = updateProductDto.image;
             product.categoryName = updateProductDto.categoryName;

            await _productRepository.UpdateProductAsync(product);
            return Ok(new {message = "Producto actualizado"});
        }

         /// <summary>
        /// Marks a product as deleted.
        /// </summary>
        /// <param name="id">The identifier of the product to be deleted.</param>
        /// <returns>An HTTP response indicating the result of the operation.</returns>
        //TODO: Delete product
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound(new { message = "El producto no existe" });
            }

            // Marca el producto como eliminado
            product.enabled = false; 
            await _productRepository.UpdateProductAsync(product); 
            return Ok(new { message = "Producto marcado como eliminado" });
        }


    }
}