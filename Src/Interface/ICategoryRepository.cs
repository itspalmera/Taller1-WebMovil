using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taller1_WebMovil.Src.DTOs.Categories;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Interface
{
    public interface ICategoryRepository
    {
        Task<List<CategoryDto>> GetCategory();
    }
}