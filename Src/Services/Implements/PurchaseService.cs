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
    /// <summary>
    /// Service implementation for managing purchase-related operations.
    /// </summary>
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PurchaseService"/> class.
        /// </summary>
        /// <param name="purchaseRepository">The repository for handling purchase-related data.</param>
        /// <param name="userRepository">The repository for handling user-related data.</param>
        public PurchaseService(IPurchaseRepository purchaseRepository,IUserRepository userRepository)
        {
            _purchaseRepository = purchaseRepository;
            _userRepository = userRepository;
        }


        /// <summary>
        /// Searches for purchases based on the user's name, with pagination and optional sorting.
        /// </summary>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="name">The name of the user to search for.</param>
        /// <param name="pageSize">The number of purchases per page.</param>
        /// <param name="sort">If true, sorts the results by purchase date in descending order.</param>
        /// <returns>A list of purchases matching the search criteria, represented as <see cref="PurchaseInfoDto"/> objects.</returns>
        public async Task<IEnumerable<PurchaseInfoDto?>> SearchPurchase(int page, string name, int pageSize,bool sort)
        {
            if(name.IsNullOrEmpty())name ="";

            var listPurchases = await _purchaseRepository.SearchPurchase(page, name ,pageSize,sort);
            var purchaseDto = listPurchases.Select(p => p!.toPurchaseDto()).ToList();
            return purchaseDto;
        }

        /// <summary>
        /// Retrieves all purchases with pagination and optional sorting.
        /// </summary>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="pageSize">The number of purchases per page.</param>
        /// <param name="sort">If true, sorts the results by purchase date in descending order.</param>
        /// <returns>A list of all purchases, represented as <see cref="PurchaseInfoDto"/> objects.</returns>
        public async Task<IEnumerable<PurchaseInfoDto?>> ViewAllPurchase(int page, int pageSize,bool sort)
        {
           var listPurchases = await _purchaseRepository.ViewAllPurchase(page,pageSize,sort);
           var purchaseDto = listPurchases.Select(p => p!.toPurchaseDto()).ToList();
            return purchaseDto;
        }


        /// <summary>
        /// Retrieves all purchases made by a specific user, with pagination.
        /// </summary>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="pageSize">The number of purchases per page.</param>
        /// <param name="rut">The RUT of the user whose purchases are to be retrieved.</param>
        /// <returns>
        /// A list of purchases made by the specified user, represented as <see cref="PurchaseInfoClientDto"/> objects.
        /// Returns null if the user is not found.
        /// </returns>
        public async Task<IEnumerable<PurchaseInfoClientDto?>> ViewAllPurchaseClient(int page, int pageSize,string email){
            var user = await _userRepository.GetUserByEmail(email);
            if(user == null) return null;
            var purchaseDto = await _purchaseRepository.ViewAllPurchaseClient(page,pageSize,user);
            return purchaseDto;
        }

        public async Task<bool> ProcessPurchase(NewPurchaseDto newPurchaseDto,string email){
            User user = await _userRepository.GetUserByEmail(email);
            if(user == null) return false;
            var result = await _purchaseRepository.ProcessPurchase(newPurchaseDto,user);
            return result;

        }
    }
}