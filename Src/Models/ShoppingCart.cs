using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Taller1_WebMovil.Src.Models
{
    [PrimaryKey(nameof(id),nameof(productId))]
    public class ShoppingCart
    {
        public int id {get; set;}
        //Relaciones
        public string userId { get; set; }
        public User user { get; set; }= null!;

        public int productId { get; set; }
        public Product product { get; set; } = null!;
    }
}