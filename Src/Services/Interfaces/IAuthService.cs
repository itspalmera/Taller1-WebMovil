using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taller1_WebMovil.Src.DTOs.Auth;

namespace Taller1_WebMovil.Src.Services.Interfaces
{
    /// <summary>
    /// Interface defining methods for authentication-related operations.
    /// </summary>
    public interface IAuthService
    {

        /// <summary>
        /// Registers a new user and generates an authentication token.
        /// </summary>
        /// <param name="registerUserDto">The details of the user to register.</param>
        /// <returns>A string representing the authentication token for the newly registered user.</returns>
        Task<string> RegisterUser(RegisterUserDto registerUserDto);

        /// <summary>
        /// Authenticates a user and generates an authentication token.
        /// </summary>
        /// <param name="loginUserDto">The login details of the user.</param>
        /// <returns>A string representing the authentication token for the authenticated user.</returns>
        Task<string> Login(LoginUserDto loginUserDto);
    }
}