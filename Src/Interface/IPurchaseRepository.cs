using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taller1_WebMovil.Src.Models;

namespace Taller1_WebMovil.Src.Interface
{
    public interface IPurchaseRepository
    {
        /// <summary>
        /// Searches for purchases based on the provided name, with pagination and optional sorting.
        /// </summary>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="name">The search term to filter purchases by name.</param>
        /// <param name="pageSize">The number of purchases per page.</param>
        /// <param name="sort">A flag indicating whether the results should be sorted.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of purchases that match the search criteria.</returns>
        Task<IEnumerable<Purchase?>> SearchPurchase(int page, string name, int pageSize, bool sort);

        /// <summary>
        /// Retrieves all purchases, with pagination and optional sorting.
        /// </summary>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="pageSize">The number of purchases per page.</param>
        /// <param name="sort">A flag indicating whether the results should be sorted.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of all purchases.</returns>
        Task<IEnumerable<Purchase?>> ViewAllPurchase(int page, int pageSize, bool sort);

        /// <summary>
        /// Retrieves all purchases for a specific client, with pagination and optional sorting.
        /// </summary>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="pageSize">The number of purchases per page.</param>
        /// <param name="user">The client whose purchases are to be retrieved.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of purchases for the specified client.</returns>
        Task<IEnumerable<Purchase?>> ViewAllPurchaseClient(int page, int pageSize, User user);
    }
}