using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Taller1_WebMovil.src.Models
{
    public class Product
    {
        public int Id {get; set;}

        [StringLength(64, MinimumLength = 10)]
        
        public required string Name {get; set;}

        [RegularExpression(@"POLERAS|GORROS|JUGEYERIA|ALIMENTCION|LIBROS")]
        public required string Type {get; set;}

        [Range(0, 100000000)]
        public int Price {get; set;}

        [Range(0, 100000)]
        public int Stock {get; set;}

        //TODO: falta atibuto image en cloudinary
    
    }
}