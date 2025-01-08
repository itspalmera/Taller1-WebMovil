using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.IdentityModel.Tokens;
using Taller1_WebMovil.Src.Data;
using Taller1_WebMovil.Src.DTOs.Auth;
using Taller1_WebMovil.Src.DTOs.User;
using Taller1_WebMovil.Src.Interface;
using Taller1_WebMovil.Src.Mapper;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Repositories
{

    /// <summary>
    /// Repository for managing user-related operations.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">The application's database context.</param>
        /// <param name="userManager">The user manager for managing user operations.</param>
         public UserRepository(ApplicationDbContext context,UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Adds a new user to the system and assigns the "Cliente" role.
        /// </summary>
        /// <param name="registerUserDto">The user registration details.</param>
        /// <returns>True if the user was created successfully, otherwise false.</returns>
        public async Task<bool> AddUser(RegisterUserDto registerUserDto)
        {
            var userDto = registerUserDto.ToUser();

            //Le asignamos la contraseña al usuario.
            await _userManager.CreateAsync(userDto, registerUserDto.password);
            // Asignamos el rol de "Cliente".
            await _userManager.AddToRoleAsync(userDto, "Cliente");    
                

           return true;
        }
        /// <summary>
        /// Changes the user's password.
        /// </summary>
        /// <param name="rut">The user's RUT.</param>
        /// <param name="changePasswordDto">The password change details.</param>
        /// <returns>True if the password was successfully changed, otherwise false.</returns>
        public async Task<bool> ChangePassword(string email,string rut, ChangePasswordDto changePasswordDto)
        {
            var user = await _context.Users.Where(u => u.rut == rut && u.Email ==email).FirstOrDefaultAsync();
            if(user == null){
                return false;
            }
            var passwordValid = await _userManager.ChangePasswordAsync(user, changePasswordDto.currentPassword, changePasswordDto.newPassword);
            if(passwordValid.Succeeded){
                return true;
            }
            return false;

        }
        /// <summary>
        /// Edits the user's details such as name, birth date, and gender.
        /// </summary>
        /// <param name="rut">The user's RUT.</param>
        /// <param name="editUser">The new user details.</param>
        /// <returns>True if the user details were updated successfully, otherwise false.</returns>
        public async Task<bool> EditUser(string rut, EditUserDto editUser)
        {
            var user = await _context.Users.Where(u => u.rut == rut).FirstOrDefaultAsync();
            if (user == null){
                return false;
            }

            user.name = editUser.name ?? user.name;

            //En el caso de que el campo birthDate de el dto editUser este vacio, null o no tenga una fecha valida, se mantendra la fecha que se tenia antes.
            if (!string.IsNullOrEmpty(editUser.birthDate) && DateOnly.TryParseExact(editUser.birthDate, "dd/MM/yyyy", out var parsedDate))
            {
                user.birthDate = parsedDate;
            }
            //En el caso de que el campo gender de el dto editUser este vacio, null o no se pueda transformar a int, se mantendra el dato que se tenia antes.
            if (!string.IsNullOrEmpty(editUser.genderId) && int.TryParse(editUser.genderId, out var parsedGender))
            {
                user.genderId = parsedGender;
            }

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }
        /// <summary>
        /// Retrieves a user by their email.
        /// </summary>
        /// <param name="Email">The user's email.</param>
        /// <returns>The user if found, otherwise null.</returns>
        public async Task<User?> GetUserByEmail(string Email)
        {
            var user = await _context.Users.Where(u => u.Email == Email).FirstOrDefaultAsync();
            return user;
        }
        /// <summary>
        /// Checks if the user has an "Admin" role.
        /// </summary>
        /// <param name="id">The user's ID.</param>
        /// <returns>True if the user is an admin, otherwise false.</returns>
        public async Task<bool> isAdmin(string id)
        {
            var role = await _context.UserRoles.Where(ur => ur.UserId == id).FirstOrDefaultAsync();
            if(role.RoleId == "1"){
                return true;
            }
            return false;
        }
        /// <summary>
        /// Toggles the user's account state (enable/disable).
        /// </summary>
        /// <param name="rut">The user's RUT.</param>
        /// <returns>A message indicating the result of the operation.</returns>
        public async Task<string> ToggleUserState(string rut)
        {
            var user = await _context.Users.Where(u=> u.rut == rut).FirstOrDefaultAsync();
            if(user == null){
                return "El usuario no existe en el sistema.";
            }            
            var role = await _context.UserRoles.Where(ur => ur.UserId == user.Id).FirstOrDefaultAsync();
            if(role.RoleId == "1"){
                return "El administrador no se puede deshabilitar.";
            }
            
            string result;
            if(user.enable == true){
                user.enable = false;
                result ="El usuario ha sido deshabilitado exitosamente.";
            }else{
                user.enable = true;
                result ="El usuario ha sido habilitado exitosamente.";
            }
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return result;
        }
        /// <summary>
        /// Verifies if the user is enabled by their email.
        /// </summary>
        /// <param name="Email">The user's email.</param>
        /// <returns>True if the user is enabled, otherwise false.</returns>
        public async Task<bool> VerifyEnableUserByEmail(string Email)
        {
            var user = await _context.Users.Where(u => u.Email == Email && u.enable == true).FirstOrDefaultAsync();
            if(user == null){
                return false;
            }
            return true;
        }
        /// <summary>
        /// Verifies if the user exists and is enabled by their email.
        /// </summary>
        /// <param name="Email">The user's email.</param>
        /// <returns>True if the user exists and is enabled, otherwise false.</returns>    
        public async Task<bool> VerifyUserByEMail(string Email)
        {
            var user = await _context.Users.Where(u => u.Email == Email && u.enable == true).FirstOrDefaultAsync();
            if(user == null){
                return false;
            }
            return true;
        }
        /// <summary>
        /// Retrieves a paginated list of users excluding the admin.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <param name="pageSize">The number of users per page.</param>
        /// <returns>A list of users.</returns>
        public async Task<IEnumerable<User?>> ViewAllUser(int page,int pageSize)
        {
            var administrador = await _context.UserRoles.Where(ur => ur.RoleId == "1").FirstOrDefaultAsync();
            int totalUser = await _context.Users.CountAsync(u=> u.Id != administrador.UserId);
            if (page < 1) page = 1;
            if (pageSize <10) pageSize =10;
            int maxPage = (int)Math.Ceiling((Double)totalUser/page);
            if (page>maxPage) page = maxPage;


            var users = await _context.Users.Where(u=> u.Id != administrador.UserId)
                                            .Include(u => u.gender)
                                            .Skip((page - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToListAsync();
            return users;   
        }
        /// <summary>
        /// Searches for users by name with pagination.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <param name="value">The search value (name).</param>
        /// <param name="pageSize">The number of users per page.</param>
        /// <returns>A list of matching users.</returns>
        public async Task<IEnumerable<User?>> SearchUser(int page,string value,int pageSize)
        {
            var administrador = await _context.UserRoles.Where(ur => ur.RoleId == "1").FirstOrDefaultAsync();
            int totalUser = await _context.Users.CountAsync(u=> u.Id != administrador.UserId);
            if (page < 1) page = 1;
            if (pageSize <10) pageSize =10;
            int maxPage = (int)Math.Ceiling((Double)totalUser/page);
            if (page>maxPage) page = maxPage;


            var users = await _context.Users.Where(u=> u.Id != administrador.UserId&& u.name.ToLower().Contains(value.ToLower()))
                                            .Include(u => u.gender)
                                            .Skip((page - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToListAsync();
            return users;   
        }
        /// <summary>
        /// Retrieves a user by their RUT.
        /// </summary>
        /// <param name="rut">The user's RUT.</param>
        /// <returns>The user if found, otherwise null.</returns>
        public async Task<User?> GetUserByRut(string rut)
        {
            var user = await _context.Users.Where(u => u.rut == rut).FirstOrDefaultAsync();
            return user;
        }
    }
}