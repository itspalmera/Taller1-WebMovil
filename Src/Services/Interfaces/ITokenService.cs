using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Services.Interfaces
{
    /// <summary>
    /// Interface defining methods for managing token-related operations.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Creates a JWT token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom the token is being created.</param>
        /// <returns>A string representation of the generated JWT token.</returns>
        Task<string> CreateToken(User user);
    }
}