using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Taller1_WebMovil.Src.Data;
using Taller1_WebMovil.Src.Models;
using Taller1_WebMovil.Src.Repositories.Interfaces;

namespace Taller1_WebMovil.Src.Repositories.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

         public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByEmail(string Email)
        {
            var user = await _context.Users.Where(u => u.Email == Email).FirstOrDefaultAsync();
            return user;
        }

        public async Task<bool> VerifyUserByEMail(string Email)
        {
            var user = await _context.Users.Where(u => u.Email == Email && u.enable == true).FirstOrDefaultAsync();
            if(user == null){
                return false;
            }
            return true;
        }
    }
}