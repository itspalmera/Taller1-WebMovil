using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Taller1_WebMovil.src.Models
{
    public class User 
    {
        [Key]
        public required string Id {get; set;}

        public required string Rut {get; set;}

        [StringLength(255, MinimumLength = 8)]
        public required string Name {get; set;}
        
        //TODO: falta Fecha de Nacimiento (string??)

        public required string Mail {get; set;}

        [RegularExpression(@"FEMENINO|MASCULINO|PREFIERO NO DECIRLO|OTRO")]
        public required string Gender {get; set;}

        [StringLength(20, MinimumLength = 8)]
        public required string Password {get; set;}
    }
}