using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Taller1_WebMovil.Src.Models
{
    [PrimaryKey(nameof(id),nameof(cartItemId))]
    public class ShoppingCart
    {
        public int id {get; set;}
        //Relaciones
        public string userId { get; set; }
        public User user { get; set; }= null!;

        public int cartItemId { get; set; }
        public CartItem cartItem { get; set; } = null!;
    }
}