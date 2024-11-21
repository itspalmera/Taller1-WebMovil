using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1_WebMovil.Src.Models
{
    public class Product
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string name { get; set; }
        public int price { get; set; }
        public int stock { get; set; }
        [Required]
        public string image {get; set;}

        //Relaciones

        public int categoryId { get; set; }
        public Category category{ get; set; } = null!;
    }
}