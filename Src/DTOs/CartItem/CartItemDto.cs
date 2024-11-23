using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.DTOs
{
    public class CartItemDto
    {
       public int quantiy {get;set;}

        //relacion
        public int productId { get; set; }
    }
}