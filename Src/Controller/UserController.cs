using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sprache;
using Taller1_WebMovil.Src.DTOs.User;
using Taller1_WebMovil.Src.Models;
using Taller1_WebMovil.Src.Services.Implements;
using Taller1_WebMovil.Src.Services.Interfaces;

namespace Taller1_WebMovil.Src.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [Authorize]
        [HttpPut("EditUser/{rut}")]
        public ActionResult<string> EditUser(string rut, [FromBody] EditUserDto editUser)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var result = _userService.EditUser(rut, editUser).Result;
                if (!result)
                {
                    return NotFound("El usuario no existe en el sistema.");
                }
                return Ok("El usuario se editó correctamente.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        [Authorize]
        [HttpPut("ChangePassword/{rut}")]
        public ActionResult<string> ChangePassword(string rut, [FromBody]ChangePasswordDto changePassword){

            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var result = _userService.ChangePassword(rut, changePassword).Result;
                if (!result)
                {
                    return NotFound("No se pudo cambiar la contraseña al usuario.");
                }
                return Ok("La contraseña se modificó correctamente.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Administrador")]
        [HttpPut("ToggleUserState/{rut}")]
        public ActionResult<string> ToggleUserState(string rut){
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var result = _userService.ToggleUserState(rut);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Administrador")]
        [HttpGet("ViewAllUser/{page}")]
        public ActionResult<IEnumerable<UserDto?>> ViewAllUser(int page, int pageSize)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var result = _userService.ViewAllUser(page,pageSize);
                return Ok(result);
                

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

        }
        [Authorize(Roles = "Administrador")]
        [HttpGet("SearchUser/{page}value")]
        public ActionResult<IEnumerable<UserDto?>> SearchUser(int page, string value, int pageSize)
        {

            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var result = _userService.SearchUser(page,value,pageSize);
                return Ok(result);
                

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}