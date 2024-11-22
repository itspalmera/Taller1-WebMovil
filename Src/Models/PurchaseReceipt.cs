using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1_WebMovil.Src.Models
{
    public class PurchaseReceipt
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string country { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public string district { get; set; }
        [Required]
        public string street { get; set; }
        public DateOnly purchaseDate { get; set; }
        public int totalPrice { get; set; }
    }
}