using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taller1_WebMovil.Src.DTOs.Auth;

namespace Taller1_WebMovil.Src.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterUser(RegisterUserDto registerUserDto);
    }
}