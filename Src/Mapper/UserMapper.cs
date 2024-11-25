using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Taller1_WebMovil.Src.DTOs.Auth;
using Taller1_WebMovil.Src.DTOs.User;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Mapper
{

    /// <summary>
    /// A static class to map objects related to users.
    /// </summary>
    public static class UserMapper
    {

        /// <summary>
        /// Maps a <see cref="RegisterUserDto"/> object to a <see cref="User"/> object.
        /// </summary>
        /// <param name="userDto">The <see cref="RegisterUserDto"/> object containing the user registration details.</param>
        /// <returns>A <see cref="User"/> object with the mapped data.</returns>
        public static User ToUser(this RegisterUserDto userDto){

            return new User
        {
            rut = userDto.rut,
            name = userDto.name,
            birthDate = DateOnly.ParseExact(userDto.birthDate, "dd/MM/yyyy"),
            Email = userDto.email.ToLower(),
            UserName = userDto.email.ToLower(),
            genderId = int.Parse(userDto.genderId),
            enable = true
        };

        }

        /// <summary>
        /// Maps a <see cref="User"/> object to a <see cref="UserDto"/> object.
        /// </summary>
        /// <param name="user">The <see cref="User"/> object to be mapped.</param>
        /// <returns>A <see cref="UserDto"/> object containing the mapped user data.</returns>
        public static UserDto toUserDto(this User user){
            return new UserDto{
                rut= user.rut,
                name = user.name,
                birthDate = user.birthDate.ToString("dd/MM/yyyy").Replace("-","/"),
                email = user.Email,
                gender = user.gender.name,
                enable= user.enable ? "Habilitado" : "Deshabilitado"
            };
        }
    }
}