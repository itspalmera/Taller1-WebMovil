using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taller1_WebMovil.Src.DTOs.Purchase;

namespace Taller1_WebMovil.Src.Services.Interfaces
{
    public interface IPurchaseService
    {
        Task<IEnumerable<PurchaseInfoDto?>> SearchPurchase(int page, string name, int pageSize,bool sort);
        Task<IEnumerable<PurchaseInfoDto?>> ViewAllPurchase(int page, int pageSize,bool sort);
        Task<IEnumerable<PurchaseInfoClientDto?>> ViewAllPurchaseClient(int page, int pageSize,string rut);
    }
}