using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taller1_WebMovil.Src.Interface;
using Taller1_WebMovil.Src.Models;
using Taller1_WebMovil.Src.DTOs.Products;


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
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            //Check if the product already exists
            bool nameExists = await _productRepository.ExistsByName(createProductDto.name);
            //TODO: Check if the category exists

            if(nameExists)
            {
                return Conflict(new {message = "El producto ya existe"});
            }

            //Create the product
            
            var newProduct = new Product
            {
                name = createProductDto.name,
                price = createProductDto.price,
                stock = createProductDto.stock,
                image = createProductDto.image,
                enabled = true,
                categoryId = createProductDto.categoryId
            };

            //Add the product to the database
            await _productRepository.AddProductAsync(newProduct);
            return CreatedAtAction(nameof(GetProductById), new {id = newProduct.id}, newProduct);

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
        [HttpGet("GetAllProduct/{page}")]
        public async Task<IActionResult> GetAllProducts(int page,[FromQuery] string? text,[FromQuery] string? category, [FromQuery] string? sort)
        {
            //var products = await _productRepository.GetAllProductsAsync(category);

            var products = await _productRepository.GetAllProductsAsync(page,text, category, sort);
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
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto updateProductDto)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if(product == null)
            {
                return NotFound(new {message = "El producto no existe"});
            }
            bool nameExist = await _productRepository.existProduct(updateProductDto.name,id);
            if(nameExist)
            {
                return Conflict(new {message = "El nombre ya existe"});
            }

            //Update the product
            product.name = updateProductDto.name;
            product.price = updateProductDto.price;
            product.stock = updateProductDto.stock;
            product.image = updateProductDto.image;
            product.enabled= true;
            product.categoryId = updateProductDto.categoryId;

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
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var result = _productRepository.DeleteProduct(id).Result;
                if (result)
                {
                    return Ok("Producto eliminado con éxito");
                }
                return BadRequest("Producto no encontrado");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}