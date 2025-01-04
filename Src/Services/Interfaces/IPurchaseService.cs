using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taller1_WebMovil.Src.DTOs.Purchase;


namespace Taller1_WebMovil.Src.Services.Interfaces
{
    /// <summary>
    /// Interface defining methods for managing purchase-related operations.
    /// </summary>
    public interface IPurchaseService
    {

        /// <summary>
        /// Searches for purchases based on the user's name, with pagination and optional sorting.
        /// </summary>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="name">The name of the user to search for.</param>
        /// <param name="pageSize">The number of purchases to display per page.</param>
        /// <param name="sort">If true, sorts the results by purchase date in descending order.</param>
        /// <returns>A list of purchases matching the search criteria, represented as <see cref="PurchaseInfoDto"/> objects.</returns>
        Task<IEnumerable<PurchaseInfoDto?>> SearchPurchase(int page, string name, int pageSize,bool sort);
        
        /// <summary>
        /// Retrieves all purchases with pagination and optional sorting.
        /// </summary>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="pageSize">The number of purchases to display per page.</param>
        /// <param name="sort">If true, sorts the results by purchase date in descending order.</param>
        /// <returns>A list of all purchases, represented as <see cref="PurchaseInfoDto"/> objects.</returns>
        Task<IEnumerable<PurchaseInfoDto?>> ViewAllPurchase(int page, int pageSize,bool sort);
        
        /// <summary>
        /// Retrieves all purchases made by a specific user, with pagination.
        /// </summary>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="pageSize">The number of purchases to display per page.</param>
        /// <param name="rut">The RUT of the user whose purchases are to be retrieved.</param>
        /// <returns>
        /// A list of purchases made by the specified user, represented as <see cref="PurchaseInfoClientDto"/> objects.
        /// </returns>
        Task<IEnumerable<PurchaseInfoClientDto?>> ViewAllPurchaseClient(int page, int pageSize,string email);

        Task<bool> ProcessPurchase(NewPurchaseDto newPurchaseDto,string email);
    }
}