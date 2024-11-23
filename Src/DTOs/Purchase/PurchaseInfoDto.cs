using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1_WebMovil.Src.DTOs.Purchase
{
    public class PurchaseInfoDto
    {
        public string purchaseDate { get; set; }
        public string idReceiptPurchase { get; set; }
        public string rut { get; set; }
        public string nameUser { get; set; }
        public string totalPrice{ get; set; }
         
    }
}