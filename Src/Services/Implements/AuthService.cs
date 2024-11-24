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
    
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<User> _signInManager;

        public AuthService(ITokenService tokenService,UserManager<User> userManager, IUserRepository userRepository,SignInManager<User> signInManager){
            _tokenService = tokenService;
            _userManager = userManager;
            _userRepository = userRepository;
            _signInManager = signInManager;
        }

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
        public async Task<string> Login(LoginUserDto loginUserDto)
        {
            string message = "Los datos ingresados son incorrectos.";
            string message2 = "El usuario está deshabilitado.";

            var user = await _userRepository.GetUserByEmail(loginUserDto.email.ToString());
            if (user is null) return message;

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginUserDto.password, false);
            if(!result.Succeeded) return message;

            var verify = await _userRepository.VerifyEnableUserByEmail(loginUserDto.email.ToString());
            if (verify is false) return message2;

            var token = await _tokenService.CreateToken(user);
            return token;

        }

    }
}