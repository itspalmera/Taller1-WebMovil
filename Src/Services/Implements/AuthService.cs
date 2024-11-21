using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Taller1_WebMovil.Src.DTOs.Auth;
using Taller1_WebMovil.Src.Mapper;
using Taller1_WebMovil.Src.Models;
using Taller1_WebMovil.Src.Repositories.Implements;
using Taller1_WebMovil.Src.Repositories.Interfaces;
using Taller1_WebMovil.Src.Services.Interfaces;

namespace Taller1_WebMovil.Src.Services.Implements
{
    
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;

        public AuthService(ITokenService tokenService,UserManager<User> userManager, IUserRepository userRepository){
            _tokenService = tokenService;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<string> RegisterUser([FromBody] RegisterUserDto registerUserDto)
        {
                if(_userRepository.VerifyUserByEMail(registerUserDto.email).Result){
                    throw new Exception("El email ingresado ya existe.");
                }
                var userDto = registerUserDto.ToUser();

                //Le asignamos la contraseña al usuario.
                await _userManager.CreateAsync(userDto, registerUserDto.password);
                // Asignamos el rol de "Cliente".
                await _userManager.AddToRoleAsync(userDto, "Cliente");
                var user = await _userRepository.GetUserByEmail(userDto.Email);
                var token = _tokenService.CreateToken(user);

                return token;
        }
    }
}