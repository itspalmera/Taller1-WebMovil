using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Taller1_WebMovil.Src.Models
{
    public class User : IdentityUser
    {
        public string rut { get; set; } = string.Empty;
        public string name { get; set; }= string.Empty;
        public DateOnly birthDate { get; set; }
        public bool enable { get; set; }


        //Relaciones
        public int genderId { get; set; }
        public Gender gender { get; set; } = null!;
    }
}