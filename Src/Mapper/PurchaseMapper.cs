using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taller1_WebMovil.Src.DTOs.Purchase;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Mapper
{
    public static class PurchaseMapper
    {
        public static PurchaseInfoDto toPurchaseDto(this Purchase purchase){
            return new PurchaseInfoDto{
            purchaseDate = purchase.purchaseReceipt.purchaseDate.ToString("dd/MM/yyyy").Replace("-","/"),
            idReceiptPurchase =purchase.purchaseReceiptId.ToString(),
            rut = purchase.user.rut,
            nameUser = purchase.user.name,
            totalPrice = purchase.purchaseReceipt.totalPrice.ToString(),
            };
        }
    }
}