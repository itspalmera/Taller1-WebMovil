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

    /// <summary>
    /// Controller for managing authentication operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController :ControllerBase
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authService">The authentication service to manage user operations.</param>
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

                var token = await _authService.Login(loginUserDto);

                if(token is null) return BadRequest(new {message ="Los datos ingresados son incorrectos."});
                if(token is "Deshabilitado") return BadRequest(new {message ="El usuario se encuentra deshabilitado."});
                    
                return Ok(new {token});
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
    }
}