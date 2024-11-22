using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.DTOs.User
{
    public class UserDto
    {
        public string rut { get; set; } = string.Empty;

        public string name { get; set; } = string.Empty;

        public string birthDate { get; set; }

        public string email { get; set; } = string.Empty;

        public string gender { get; set;} = null!;

        public string enable { get; set; }
    }
}