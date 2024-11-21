using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taller1_WebMovil.Src.DTOs.Auth;
using Taller1_WebMovil.Src.DTOs.User;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> VerifyUserByEMail(string Email);
        Task<User?> GetUserByEmail(string Email);
        Task<bool> VerifyEnableUserByEmail(string Email);
        Task<bool> EditUser(string rut, EditUserDto editUser);
        Task<bool> ChangePassword(string rut, ChangePasswordDto changePasswordDto);
        Task<bool> AddUser(RegisterUserDto registerUserDto);
    }
}