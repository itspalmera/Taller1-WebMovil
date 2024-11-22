using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taller1_WebMovil.Src.DTOs.User;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> EditUser(string rut, EditUserDto editUser);
        Task<bool> ChangePassword(string rut, ChangePasswordDto changePasswordDto);
        Task<string> ToggleUserState(string rut);
        Task<IEnumerable<UserDto?>> SearchUser(int page, string value, int pageSize);
        Task<IEnumerable<UserDto?>> ViewAllUser(int page, int pageSize);
    }
}