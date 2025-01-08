using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taller1_WebMovil.Src.Interface;

namespace Taller1_WebMovil.Src.Controller
{

    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
         private readonly IGenderRepository _genderRepository;  

        public GenderController(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }

        [HttpGet("GetGenders")]
        public async Task<IActionResult> GetGenders()
        {
            var gender = await _genderRepository.GetGenders();
            return Ok(gender);
        }
    }
}