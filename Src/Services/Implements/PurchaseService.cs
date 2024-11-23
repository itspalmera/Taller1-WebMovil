using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Taller1_WebMovil.Src.DTOs.Purchase;
using Taller1_WebMovil.Src.Interface;
using Taller1_WebMovil.Src.Mapper;
using Taller1_WebMovil.Src.Models;
using Taller1_WebMovil.Src.Services.Interfaces;

namespace Taller1_WebMovil.Src.Services.Implements
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseService(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public async Task<IEnumerable<PurchaseInfoDto?>> SearchPurchase(int page, string name, int pageSize,bool sort)
        {
            if(name.IsNullOrEmpty())name ="";

            var listPurchases = await _purchaseRepository.SearchPurchase(page, name ,pageSize,sort);
            var purchaseDto = listPurchases.Select(p => p!.toPurchaseDto()).ToList();
            return purchaseDto;
        }

        public async Task<IEnumerable<PurchaseInfoDto?>> ViewAllPurchase(int page, int pageSize,bool sort)
        {
           var listPurchases = await _purchaseRepository.ViewAllPurchase(page,pageSize,sort);
           var purchaseDto = listPurchases.Select(p => p!.toPurchaseDto()).ToList();
            return purchaseDto;
        }
    }
}