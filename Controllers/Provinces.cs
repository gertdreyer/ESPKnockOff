using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESPKnockOff.Models;
using ESPKnockOff.Services;

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
            //return _context.Province.ToList();
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
        public async Task<ActionResult<List<Municipalities>>> GetProvinceMunicipalities(int id)
        {
            // TODO: Get municipalities in province.
            var municipalities = new List<Municipalities>();
            return municipalities;
        }

        [HttpPost]
        public async Task<ActionResult> AddProvince(Province province)
        {
            try
            {
                _dbService.Insert(province);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProvince(Province province)
        {
            try
            {
                _dbService.Update(province);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveProvince(Province province)
        {
            try
            {
                _dbService.Remove(province);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
