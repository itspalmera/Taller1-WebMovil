using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taller1_WebMovil.Src.DTOs.User;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Services.Interfaces
{
    /// <summary>
    /// Interface defining methods for managing user-related operations.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Edits the user's details, such as name, birth date, and gender.
        /// </summary>
        /// <param name="rut">The RUT of the user to edit.</param>
        /// <param name="editUser">The new user details.</param>
        /// <returns>True if the user's details were successfully updated, otherwise false.</returns>
        Task<bool> EditUser(string rut, EditUserDto editUser);

         /// <summary>
        /// Changes the user's password.
        /// </summary>
        /// <param name="rut">The RUT of the user whose password is being changed.</param>
        /// <param name="changePasswordDto">The password change details.</param>
        /// <returns>True if the password was successfully changed, otherwise false.</returns>
        Task<bool> ChangePassword(string email,string rut, ChangePasswordDto changePasswordDto);
        
        /// <summary>
        /// Toggles the user's state between enabled and disabled.
        /// </summary>
        /// <param name="rut">The RUT of the user whose state is to be toggled.</param>
        /// <returns>A string message indicating the result of the operation.</returns>
        Task<string> ToggleUserState(string rut);

        /// <summary>
        /// Searches for users by name with pagination.
        /// </summary>
        /// <param name="page">The current page number for pagination.</param>
        /// <param name="value">The search value (user name or part of it).</param>
        /// <param name="pageSize">The number of users per page.</param>
        /// <returns>A list of users matching the search criteria, represented as <see cref="UserDto"/> objects.</returns>
        Task<IEnumerable<UserDto?>> SearchUser(int page, string value, int pageSize);
        
        /// <summary>
        /// Retrieves a paginated list of all users.
        /// </summary>
        /// <param name="page">The current page number for pagination.</param>
        /// <param name="pageSize">The number of users per page.</param>
        /// <returns>A list of all users, represented as <see cref="UserDto"/> objects.</returns>
        Task<IEnumerable<UserDto?>> ViewAllUser(int page, int pageSize);
    }
}