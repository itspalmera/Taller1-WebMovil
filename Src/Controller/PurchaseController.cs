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

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

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

    }
}