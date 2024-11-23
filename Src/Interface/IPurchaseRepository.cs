using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Interface
{
    public interface IPurchaseRepository
    {
        Task<IEnumerable<Purchase?>> SearchPurchase(int page,string name, int pageSize,bool sort);
        Task<IEnumerable<Purchase?>> ViewAllPurchase(int page, int pageSize,bool sort);
    }
}