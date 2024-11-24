using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Taller1_WebMovil.Src.DTOs.Auth;
using Taller1_WebMovil.Src.Models;
using Taller1_WebMovil.Src.Services.Interfaces;

namespace Taller1_WebMovil.Src.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController :ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService){
           _authService = authService;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registerUserDto">The user registration details.</param>
        /// <returns>A confirmation message.</returns>
        /// <response code="200">Returns the confirmation message.</response>
        /// <response code="400">If there was an error with the request.</response>
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterUserDto registerUserDto){
            try{
                if(!ModelState.IsValid) return BadRequest(ModelState);
                
                var response = await _authService.RegisterUser(registerUserDto);
                return Ok(response);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Logs in an existing user.
        /// </summary>
        /// <param name="loginUserDto">The user login details.</param>
        /// <returns>The logged-in user details.</returns>
        /// <response code="200">Returns the logged-in user details.</response>
        /// <response code="400">If there was an error with the request.</response>
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserDto loginUserDto){
            try{
                if(!ModelState.IsValid) return BadRequest(ModelState);

                var response = await _authService.Login(loginUserDto);

                if(response is null) return BadRequest("Datos incorrectos.");
                    return Ok(response);
                }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
    }
}