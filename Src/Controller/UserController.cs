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

    /// <summary>
    /// Controller for managing user-related operations.
    /// </summary
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">The service used to handle user-related logic.</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Edits a user's information.
        /// </summary>
        /// <param name="rut">The user rut.</param>
        /// <param name="editUserDto">The updated user details.</param>
        /// <returns>A confirmation message.</returns>
        /// <response code="200">Returns a confirmation message.</response>
        /// <response code="400">If there was an error with the request.</response>
        /// <response code="404">If the user is not found.</response>
        [Authorize]
        [HttpPut("EditUser/{rut}")]
        public ActionResult<string> EditUser(string rut, [FromBody] EditUserDto editUserDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var result = _userService.EditUser(rut, editUserDto).Result;
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

        /// <summary>
        /// Changes a user's password.
        /// </summary>
        /// <param name="rut">The user rut.</param>
        /// <param name="changePasswordDto">The new password details.</param>
        /// <returns>A confirmation message.</returns>
        /// <response code="200">Returns a confirmation message.</response>
        /// <response code="400">If there was an error with the request.</response>
        /// <response code="404">If the user is not found.</response>
        [Authorize]
        [HttpPut("ChangePassword/{rut}")]
        public ActionResult<string> ChangePassword(string rut, [FromBody]ChangePasswordDto changePasswordDto){

            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                string email = User.Identity.Name;
                var result = _userService.ChangePassword(email,rut, changePasswordDto).Result;
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
        /// <summary>
        /// Changes the state of a user.
        /// </summary>
        /// <param name="rut">The user rut.</param>
        /// <returns>A confirmation message.</returns>
        /// <response code="200">Returns a confirmation message.</response>
        /// <response code="400">If there was an error with the request.</response>
        /// <response code="404">If the user was not found.</response>
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
        
        /// <summary>
        /// Retrieves a paginated list of all users.
        // </summary>
        /// <param name="page">The current page number.</param>
        /// <param name="pageSize">The number of users per page.</param>
        /// <returns>A paginated list of users.</returns>
        /// <response code="200">Returns the list of users.</response>
        /// <response code="400">If there was an error with the request.</response>
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

        /// <summary>
        /// Searches for users by name and retrieves a paginated result.
        /// </summary>
        /// <param name="page">The current page number.</param>
        /// <param name="name">The search term to filter users.</param>
        /// <param name="pageSize">The number of users per page.</param>
        /// <returns>A paginated list of users matching the search criteria.</returns>
        /// <response code="200">Returns the list of users matching the search.</response>
        /// <response code="400">If there was an error with the request.</response>
        [Authorize(Roles = "Administrador")]
        [HttpGet("SearchUser/{page}&name={name}")]
        public ActionResult<IEnumerable<UserDto?>> SearchUser(int page, string name, int pageSize)
        {

            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var result = _userService.SearchUser(page,name,pageSize);
                return Ok(result);
                

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}