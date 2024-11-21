using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> VerifyUserByEMail(string Email);
        Task<User?> GetUserByEmail(string Email);
        Task<bool> VerifyEnableUserByEmail(string Email);
    }
}