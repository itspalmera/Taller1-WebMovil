using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1_WebMovil.Src.Models
{
    public class User
    {
        [Key]
        public int id {get; set;}
        public string rut { get; set; } = string.Empty;
        public string name { get; set; }= string.Empty;
        public DateTime birthDate { get; set; }
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public bool enable { get; set; }


        //Relaciones
        public int roleId { get; set; }
        public Role role { get; set; } = null!;

        public int genderId { get; set; }
        public Gender gender { get; set; } = null!;
    }
}