using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Taller1_WebMovil.Src.Data;
using Taller1_WebMovil.Src.DTOs.Genders;
using Taller1_WebMovil.Src.Interface;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Repositories
{
    public class GenderRepository: IGenderRepository
    {
        private readonly ApplicationDbContext _context;
        public GenderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GenderDto>> GetGenders()
        {
            IQueryable<Gender> query = _context.Genders.AsQueryable();
            var genders = await query.Select(gender => new GenderDto
            {
                id = gender.id.ToString(),
                name = gender.name
            }).ToListAsync();

            return genders;
        }
    }
}