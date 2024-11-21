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
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            // Cerrar la sesión del usuario
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            // Redirigir al usuario a la página de inicio de sesión
            return RedirectToAction("login");
        }


        
    }
}