using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taller1_WebMovil.Src.Interface;

namespace Taller1_WebMovil.Src.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;  

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet("GetCategory")]
        public async Task<IActionResult> GetCategory()
        {
            var categories = await _categoryRepository.GetCategory();
            return Ok(categories);
        }
        
    }
}