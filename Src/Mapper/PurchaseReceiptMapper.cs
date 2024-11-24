using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taller1_WebMovil.Src.DTOs.Purchase;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Mapper
{
    public static class PurchaseReceiptMapper
    {
        public static PurchaseReceipt toPurchaseReceipt(this NewPurchaseDto newPurchaseDto){
            return new PurchaseReceipt{
                country =newPurchaseDto.country,
                city =newPurchaseDto.city,
                district =newPurchaseDto.district,
                street =newPurchaseDto.street
            };
        }
    }
}