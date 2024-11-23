using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1_WebMovil.Src.Models
{
    public class CartItem
    {
        [Key]
        public int id {get; set;}
        
        public int quantity {get;set;}

        //relacion
        public int productId { get; set; }
        public Product product { get; set; } = null!;
    }
}