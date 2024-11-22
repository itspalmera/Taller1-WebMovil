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
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        //CREATE crear un recurso 🡪 POST
        //READ leer u obtener un recurso 🡪 GET
        //UPDATE actualizar un recurso 🡪 PUT
        //DELETE eliminar un recurso 🡪 DELETE

        //TODO: Add product
        [HttpPost("")]
        [Authorize(Roles = "Admin")]
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
                category = createProductDto.category
            };

            //Add the product to the database
            await _productRepository.AddProductAsync(newProduct);
            return CreatedAtAction(nameof(GetProductById), new {id = newProduct.id}, newProduct);

        }

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

        //TODO: Get all products
        [HttpGet("")]
        public async Task<IActionResult> GetAllProducts([FromQuery] string? text,[FromQuery] string? category, [FromQuery] string? sort)
        {
            //var products = await _productRepository.GetAllProductsAsync(category);

            var products = await _productRepository.GetAllProductsAsync(text, category, sort);
            return Ok(products);
        }
        
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
            product.category = updateProductDto.category;

            await _productRepository.UpdateProductAsync(product);
            return Ok(new {message = "Producto actualizado"});
        }

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
            product.enabled = true; 
            await _productRepository.UpdateProductAsync(product); 
            return Ok(new { message = "Producto marcado como eliminado" });
        }


    }
}