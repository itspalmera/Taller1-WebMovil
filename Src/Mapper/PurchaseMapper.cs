using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taller1_WebMovil.Src.DTOs.Products;
using Taller1_WebMovil.Src.DTOs.Purchase;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Mapper
{

    /// <summary>
    /// A static class to map objects related to purchases.
    /// </summary>
    public static class PurchaseMapper
    {

        /// <summary>
        /// Maps a <see cref="Purchase"/> object to a <see cref="PurchaseInfoDto"/> object.
        /// </summary>
        /// <param name="purchase">The <see cref="Purchase"/> object to be mapped.</param>
        /// <returns>A <see cref="PurchaseInfoDto"/> object containing the mapped data.</returns>
        public static PurchaseInfoDto toPurchaseDto(this Purchase purchase){
            return new PurchaseInfoDto{
            purchaseDate = purchase.purchaseReceipt.purchaseDate.ToString("dd/MM/yyyy").Replace("-","/"),
            idReceiptPurchase =purchase.purchaseReceiptId.ToString(),
            rut = purchase.user.rut,
            nameUser = purchase.user.name,
            totalPrice = purchase.purchaseReceipt.totalPrice.ToString(),
            };
        }

        /// <summary>
        /// Maps a <see cref="Purchase"/> object to a <see cref="PurchaseInfoClientDto"/> object.
        /// </summary>
        /// <param name="purchase">The <see cref="Purchase"/> object to be mapped.</param>
        /// <returns>A <see cref="PurchaseInfoClientDto"/> object containing the mapped data.</returns>
        public static PurchaseInfoClientDto toPurchaseInfoClientDto(this Purchase purchase)
        {

            return new PurchaseInfoClientDto
            {
                purchaseDate = purchase.purchaseReceipt.purchaseDate.ToString("dd/MM/yyyy").Replace("-", "/"),
                totalPurchasePrice = purchase.purchaseReceipt.totalPrice,
                ProductDetails = new List<ProductInfoClientDto>
                {
                    new ProductInfoClientDto
                    {
                        nameProduct = purchase.product.name,
                        Type = purchase.product.category.name,
                        price = purchase.product.price.ToString(),
                        quantity = purchase.quantity.ToString(),
                        totalPrice = (purchase.product.price * purchase.quantity).ToString()
                    }
                }
            };
        }
    }
}