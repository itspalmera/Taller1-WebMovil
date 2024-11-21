using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Taller1_WebMovil.Src.DTOs.Auth;
using Taller1_WebMovil.Src.Models;
using Taller1_WebMovil.Src.Services.Interfaces;

namespace Taller1_WebMovil.Src.Controller
{
    public class AuthController :ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService){
           _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserDto registerUserDto){
            try{
                if(!ModelState.IsValid) return BadRequest(ModelState);
                
                var response = await _authService.RegisterUser(registerUserDto);
                return Ok(response);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        
    }
}