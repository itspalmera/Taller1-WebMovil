using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Taller1_WebMovil.Src.Data;
using Taller1_WebMovil.Src.DTOs.Categories;
using Taller1_WebMovil.Src.Interface;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDto>> GetCategory()
        {
            IQueryable<Category> query = _context.Categories.AsQueryable();
            var categories = await query.Select(category => new CategoryDto
            {
                id = category.id.ToString(),
                name = category.name
            }).ToListAsync();

            return categories;
        }

    }
}
