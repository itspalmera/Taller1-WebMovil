using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1_WebMovil.Src.DTOs.Products
{
    public class ProductDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string price { get; set; }
        public string stock { get; set; }
        public string image { get; set; }
        public string enabled { get; set; }
        public string type { get; set; }
    }
}