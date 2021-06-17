using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESPKnockOff.Models;
using ESPKnockOff.Services;
using ESPKnockOff.Data;
using Microsoft.AspNetCore.Authorization;
namespace ESPKnockOff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Provinces : Controller
    {
        private readonly DatabaseService _dbService;

        public Provinces(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<List<Province>> GetProvinces()
        {
            return await _dbService.GetObjects<Province>();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Province>> GetProvince(int id)
        {
            var province = await _dbService.GetObjectById<Province>(id);

            if (province == null)
            {
                return NotFound();
            }

            return province;
        }

        [HttpGet("{id}/municipalities")]
        public async Task<List<Municipality>> GetProvinceMunicipalities(int id)
        {
            return await _dbService.GetObjectSubObjects<Province, Municipality>(id);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddProvince(Province province)
        {
            try
            {
                var result = _dbService.Update(province);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateProvince(Province province)
        {
            try
            {
                var result = _dbService.Update(province);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
