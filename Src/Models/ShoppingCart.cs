using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Taller1_WebMovil.Src.Models
{
    
    public class ShoppingCart
    {
        [Key]
        public int id {get; set;}
        //Relaciones
        public string userId { get; set; }
        public User user { get; set; }= null!;

        public List<CartItem> Items { get; set; } = new();
    }
}