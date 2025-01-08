using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taller1_WebMovil.Src.DTOs.Auth;
using Taller1_WebMovil.Src.DTOs.User;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Interface
{
    /// <summary>
    /// Interface for managing user-related operations.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Verifies if a user exists by email.
        /// </summary>
        /// <param name="Email">The email of the user to verify.</param>
        /// <returns>A task that returns a boolean indicating whether the user exists.</returns>
        Task<bool> VerifyUserByEMail(string Email);

        /// <summary>
        /// Retrieves a user by email.
        /// </summary>
        /// <param name="Email">The email of the user to retrieve.</param>
        /// <returns>A task that returns the user if found, or null if not.</returns>
        Task<User?> GetUserByEmail(string Email);

        /// <summary>
        /// Retrieves a user by their Rut (Chilean national identification number).
        /// </summary>
        /// <param name="rut">The Rut of the user to retrieve.</param>
        /// <returns>A task that returns the user if found, or null if not.</returns>
        Task<User?> GetUserByRut(string rut);

        /// <summary>
        /// Verifies if a user is enabled by email.
        /// </summary>
        /// <param name="Email">The email of the user to verify.</param>
        /// <returns>A task that returns a boolean indicating whether the user is enabled.</returns>
        Task<bool> VerifyEnableUserByEmail(string Email);

        /// <summary>
        /// Edits a user's details.
        /// </summary>
        /// <param name="rut">The Rut of the user to edit.</param>
        /// <param name="editUser">The new details of the user.</param>
        /// <returns>A task that returns a boolean indicating whether the operation was successful.</returns>
        Task<bool> EditUser(string rut, EditUserDto editUser);

        /// <summary>
        /// Changes a user's password.
        /// </summary>
        /// <param name="rut">The Rut of the user whose password is being changed.</param>
        /// <param name="changePasswordDto">The new password details.</param>
        /// <returns>A task that returns a boolean indicating whether the password change was successful.</returns>
        Task<bool> ChangePassword(string email,string rut, ChangePasswordDto changePasswordDto);

        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="registerUserDto">The details of the user to register.</param>
        /// <returns>A task that returns a boolean indicating whether the user was added successfully.</returns>
        Task<bool> AddUser(RegisterUserDto registerUserDto);

        /// <summary>
        /// Toggles the enabled state of a user.
        /// </summary>
        /// <param name="rut">The Rut of the user whose state is to be toggled.</param>
        /// <returns>A task that returns the new state of the user (enabled or disabled).</returns>
        Task<string> ToggleUserState(string rut);

        /// <summary>
        /// Searches for users based on a search value (e.g., name, email).
        /// </summary>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="value">The search value.</param>
        /// <param name="pageSize">The number of users to return per page.</param>
        /// <returns>A task that returns a collection of users matching the search criteria.</returns>
        Task<IEnumerable<User?>> SearchUser(int page, string value, int pageSize);

        /// <summary>
        /// Retrieves all users with pagination.
        /// </summary>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="pageSize">The number of users to return per page.</param>
        /// <returns>A task that returns a collection of users.</returns>
        Task<IEnumerable<User?>> ViewAllUser(int page, int pageSize);
    }
}