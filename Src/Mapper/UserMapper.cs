using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Taller1_WebMovil.Src.DTOs.Auth;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Mapper
{
    public static class UserMapper
    {
        public static User ToUser(this RegisterUserDto userDto){

            return new User
        {
            rut = userDto.rut,
            name = userDto.name,
            birthDate = DateOnly.ParseExact(userDto.birthDate, "dd/MM/yyyy"),
            Email = userDto.email.ToLower(),
            UserName = userDto.email.ToLower(),
            genderId = int.Parse(userDto.gender),
            enable = true
        };
        }
    }
}