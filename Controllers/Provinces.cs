using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
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
        public async Task<ActionResult<List<Province>>> GetProvinces()
        {
            var provinces = await _dbService.GetObjects<Province>();

            int ETag = 0;
            foreach (var province in provinces)
            {
                ETag += province.GetHashCode();
            }

            if (Request.Headers["ETag"] != ETag.ToString())
            {
                Response.Headers.Add("ETag", ETag.ToString());
                return provinces;
            }
            else
            {
                return StatusCode(304);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Province>> GetProvince(int id)
        {
            var province = await _dbService.GetObjectById<Province>(id);
            var provinceETag = province.GetHashCode().ToString();

            if (province == null)
            {
                return NotFound();
            }
            else if (Request.Headers["ETag"] != provinceETag)
            {
                Response.Headers.Add("ETag", provinceETag);
                return province;
            }
            else
            {
                return StatusCode(304);
            }
        }

        [HttpGet("{id}/municipalities")]
        public async Task<ActionResult<List<Municipality>>> GetProvinceMunicipalities(int id)
        {
            try
            {
                var municipalities = await _dbService.GetObjectSubObjects<Province, Municipality>(id);

                int etag = 0;
                foreach (var municipality in municipalities)
                {
                    etag += municipality.GetHashCode();
                }

                if (Request.Headers["ETag"] != etag.ToString())
                {
                    Response.Headers.Add("ETag", etag.ToString());
                    return municipalities;
                }
                else
                {
                    return StatusCode(304);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            
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
                var resultETag = result.GetHashCode().ToString();

                if (Request.Headers["ETag"] != resultETag)
                {
                    Response.Headers.Add("ETag", resultETag);
                    return Ok(result);
                }
                else
                {
                    return StatusCode(304);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
