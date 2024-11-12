using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Taller1_WebMovil.src.Models;

namespace Taller1_WebMovil.Src.Models
{
    [PrimaryKey(nameof(productId), nameof(userId), nameof(purchaseReceiptId))]
    public class Purchase
    {
        public int quantity { get; set; }
        public int totalPrice { get; set; }

        //Relaciones
        public int productId { get; set; }
        public Product product { get; set; } = null!;
        public int userId { get; set; }
        public User user { get; set; } = null!;

        public int purchaseReceiptId { get; set; }
        public PurchaseReceipt purchaseReceipt { get; set; } = null!;
    }
}