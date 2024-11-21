using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Taller1_WebMovil.Src.DTOs.User;
using Taller1_WebMovil.Src.Models;
using Taller1_WebMovil.Src.Repositories.Interfaces;
using Taller1_WebMovil.Src.Services.Interfaces;

namespace Taller1_WebMovil.Src.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<bool> ChangePassword(string rut, ChangePasswordDto changePasswordDto)
        {
            var result = await _userRepository.ChangePassword(rut,changePasswordDto);
            return result;
        }

        public async Task<bool> EditUser(string rut, EditUserDto editUser)
        {
            var result = await _userRepository.EditUser(rut, editUser);
            return result;
        }
    }
}