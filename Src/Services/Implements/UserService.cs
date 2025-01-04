using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Taller1_WebMovil.Src.DTOs.User;
using Taller1_WebMovil.Src.Mapper;
using Taller1_WebMovil.Src.Models;
using Taller1_WebMovil.Src.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Taller1_WebMovil.Src.Interface;

namespace Taller1_WebMovil.Src.Services.Implements
{
    /// <summary>
    /// Service implementation for managing user-related operations.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepository">The repository for handling user data.</param>
        /// <param name="userManager">The UserManager for managing user operations.</param>
        public UserService(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        /// <summary>
        /// Changes the user's password.
        /// </summary>
        /// <param name="rut">The RUT of the user whose password is being changed.</param>
        /// <param name="changePasswordDto">The password change details.</param>
        /// <returns>True if the password was successfully changed, otherwise false.</returns>
        public async Task<bool> ChangePassword(string email,string rut, ChangePasswordDto changePasswordDto)
        {
            var result = await _userRepository.ChangePassword(email,rut,changePasswordDto);
            return result;
        }

        /// <summary>
        /// Edits the user's details such as name, birth date, and gender.
        /// </summary>
        /// <param name="rut">The RUT of the user to edit.</param>
        /// <param name="editUser">The new user details.</param>
        /// <returns>True if the user details were successfully updated, otherwise false.</returns>
        public async Task<bool> EditUser(string rut, EditUserDto editUser)
        {
            var result = await _userRepository.EditUser(rut, editUser);
            return result;
        }

        /// <summary>
        /// Toggles the user's state between enabled and disabled.
        /// </summary>
        /// <param name="rut">The RUT of the user to toggle state.</param>
        /// <returns>A string message indicating the result of the operation.</returns>
        public async Task<string> ToggleUserState(string rut)
        {
            var result = await _userRepository.ToggleUserState(rut);
            return result;

        }

        /// <summary>
        /// Searches for users by name with pagination.
        /// </summary>
        /// <param name="page">The current page number for pagination.</param>
        /// <param name="value">The search value (user name or part of it).</param>
        /// <param name="pageSize">The number of users per page.</param>
        /// <returns>A list of users matching the search criteria, represented as <see cref="UserDto"/> objects.</returns>
        public async Task<IEnumerable<UserDto?>> SearchUser(int page, string? value, int pageSize)
        {
            if(value.IsNullOrEmpty())value ="";

            var listUsers = await _userRepository.SearchUser(page, value,pageSize);
            var usersDto = listUsers.Select(u => u!.toUserDto()).ToList();
            return usersDto;
        }


        /// <summary>
        /// Retrieves a paginated list of all users.
        /// </summary>
        /// <param name="page">The current page number for pagination.</param>
        /// <param name="pageSize">The number of users per page.</param>
        /// <returns>A list of all users, represented as <see cref="UserDto"/> objects.</returns>
        public async Task<IEnumerable<UserDto?>> ViewAllUser(int page, int pageSize)
        {
            var listUsers = await _userRepository.ViewAllUser(page,pageSize);
            var usersDto = listUsers.Select(u => u!.toUserDto()).ToList();
            return usersDto;
        }
    }
}