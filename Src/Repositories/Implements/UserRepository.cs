using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Taller1_WebMovil.Src.Data;
using Taller1_WebMovil.Src.DTOs.Auth;
using Taller1_WebMovil.Src.DTOs.User;
using Taller1_WebMovil.Src.Mapper;
using Taller1_WebMovil.Src.Models;
using Taller1_WebMovil.Src.Repositories.Interfaces;

namespace Taller1_WebMovil.Src.Repositories.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

         public UserRepository(ApplicationDbContext context,UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> AddUser(RegisterUserDto registerUserDto)
        {
            var userDto = registerUserDto.ToUser();

            //Le asignamos la contraseña al usuario.
            await _userManager.CreateAsync(userDto, registerUserDto.password);
            // Asignamos el rol de "Cliente".
            await _userManager.AddToRoleAsync(userDto, "Cliente");    
                

           return true;
        }

        

        public async Task<User?> GetUserByEmail(string Email)
        {
            var user = await _context.Users.Where(u => u.Email == Email).FirstOrDefaultAsync();
            return user;
        }

        public async Task<bool> VerifyEnableUserByEmail(string Email)
        {
            var user = await _context.Users.Where(u => u.Email == Email && u.enable == true).FirstOrDefaultAsync();
            if(user == null){
                return false;
            }
            return true;
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