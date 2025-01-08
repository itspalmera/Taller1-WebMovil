using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller1_WebMovil.Src.DTOs.Auth;
using Taller1_WebMovil.Src.Interface;
using Taller1_WebMovil.Src.Mapper;
using Taller1_WebMovil.Src.Models;
using Taller1_WebMovil.Src.Services.Interfaces;

namespace Taller1_WebMovil.Src.Services.Implements
{
    /// <summary>
    /// Service implementation for managing authentication-related operations.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<User> _signInManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="tokenService">Service for handling token generation.</param>
        /// <param name="userManager">UserManager for managing user-related operations.</param>
        /// <param name="userRepository">Repository for user-related data.</param>
        /// <param name="signInManager">SignInManager for managing user sign-in operations.</param>
        public AuthService(ITokenService tokenService,UserManager<User> userManager, IUserRepository userRepository,SignInManager<User> signInManager){
            _tokenService = tokenService;
            _userManager = userManager;
            _userRepository = userRepository;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Registers a new user and returns an authentication token.
        /// </summary>
        /// <param name="registerUserDto">The user registration details.</param>
        /// <returns>A string containing the authentication token.</returns>
        /// <exception cref="Exception">Thrown if the email already exists or if registration fails.</exception>
        public async Task<string> RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
            if (_userRepository.VerifyUserByEMail(registerUserDto.email).Result)
            {
                throw new Exception("El email ingresado ya existe.");
            }
            var userRegistered = await _userRepository.AddUser(registerUserDto);
            if (userRegistered)
            {
                var user = await _userRepository.GetUserByEmail(registerUserDto.email);
                var token = await _tokenService.CreateToken(user);

                return token;
            }
            throw new Exception("No se pudo registrar el usuario.");
        }

        /// <summary>
        /// Authenticates a user and returns an authentication token.
        /// </summary>
        /// <param name="loginUserDto">The user login details.</param>
        /// <returns>A string containing the authentication token, or an error message if authentication fails.</returns>
        public async Task<string> Login(LoginUserDto loginUserDto)
        {
            string message = "Deshabilitado";

            var user = await _userRepository.GetUserByEmail(loginUserDto.email.ToString());
            if (user is null) return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginUserDto.password, false);
            if(!result.Succeeded) return null;

            var verify = await _userRepository.VerifyEnableUserByEmail(loginUserDto.email.ToString());
            if (verify is false) return message;

            var token = await _tokenService.CreateToken(user);
            return token;

        }

    }
}