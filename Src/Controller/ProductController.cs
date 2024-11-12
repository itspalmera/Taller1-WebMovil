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

        //Add product
        [HttpPost("")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            //Check if the product already exists
            bool nameExists = await _productRepository.ExistsByCode(createProductDto.name);
            bool categoryExists = await _productRepository.ExistsByCode(createProductDto.category.name);

            if(nameExists || categoryExists)
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
        

    


    }
}