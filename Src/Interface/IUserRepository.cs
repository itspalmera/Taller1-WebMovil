using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taller1_WebMovil.src.Models;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.src.Interface
{
    public interface IUserRepository
    {
        Task<bool> ExistsByCode(string code);
        Task<User> AddUserAsync(User user);
        //Task<User?> GetUserByIdAsync(int id);
        //Task<List<User>> GetAllUsersAsync(string? sort, string? gender);
        Task DeleteUserAsync(User user);
        Task UpdateUserAsync(User user);
    }
}