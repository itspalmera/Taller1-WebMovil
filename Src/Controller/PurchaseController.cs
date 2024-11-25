using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taller1_WebMovil.Src.DTOs.Purchase;
using Taller1_WebMovil.Src.Services.Implements;
using Taller1_WebMovil.Src.Services.Interfaces;

namespace Taller1_WebMovil.Src.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PurchaseController"/> class.
        /// </summary>
        /// <param name="purchaseService">The service to handle purchase operations.</param>
        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }
        /// <summary>
        /// Retrieves a paginated list of all purchases with optional sorting.
        /// </summary>
        /// <param name="page">The current page number.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <param name="sort">Indicates whether to sort the results.</param>
        /// <returns>A list of purchase information (<see cref="PurchaseInfoDto"/>).</returns>
        /// <response code="200">Returns the list of purchases.</response>
        /// <response code="400">If the request is invalid.</response>
        [Authorize(Roles = "Administrador")]
        [HttpGet("ViewAllPurchase/{page}")]
        public ActionResult<IEnumerable<PurchaseInfoDto?>> ViewAllPurchase(int page, int pageSize,bool sort)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var result = _purchaseService.ViewAllPurchase(page,pageSize,sort);
                return Ok(result);
                

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

        }
        /// <summary>
        /// Searches for purchases based on the provided name, with pagination and optional sorting.
        /// </summary>
        /// <param name="page">The current page number.</param>
        /// <param name="name">The name or keyword to search for.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <param name="sort">Indicates whether to sort the results.</param>
        /// <returns>A list of purchase information (<see cref="PurchaseInfoDto"/>).</returns>
        /// <response code="200">Returns the list of matching purchases.</response>
        /// <response code="400">If the request is invalid.</response>
        [Authorize(Roles = "Administrador")]
        [HttpGet("SearchPurchase/{page}")]
        public ActionResult<IEnumerable<PurchaseInfoDto?>> SearchPurchase(int page,string name, int pageSize,bool sort)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var result = _purchaseService.SearchPurchase(page,name,pageSize,sort);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Retrieves a paginated list of purchases for a specific client.
        /// </summary>
        /// <param name="page">The current page number.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <param name="rut">The client's unique identifier (RUT).</param>
        /// <returns>A list of purchase information (<see cref="PurchaseInfoDto"/>).</returns>
        /// <response code="200">Returns the list of purchases for the client.</response>
        /// <response code="400">If the request is invalid.</response>
        /// <response code="404">If the client does not exist.</response>
        [Authorize(Roles = "Cliente")]
        [HttpGet("ViewAllPurchaseClient/{rut}")]
        public ActionResult<IEnumerable<PurchaseInfoDto?>> ViewAllPurchaseClient(int page, int pageSize,string rut)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var result = _purchaseService.ViewAllPurchaseClient(page,pageSize,rut);
                if(result==null)NotFound("El cliente no existe.");
                return Ok(result);
                

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

        }

    }
}