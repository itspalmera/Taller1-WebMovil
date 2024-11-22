using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.DTOs.Products
{
    public class CreateProductDto
    {
        [Required]
        public string name { get; set; }
        [Required]
        public int price { get; set; }
        [Required]
        public int stock { get; set; }
        [Required]
        public string image {get; set;}
        public Category category{ get; set; }
    }
}