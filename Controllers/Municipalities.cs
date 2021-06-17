using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ESPKnockOff.Models;
using ESPKnockOff.Services;
using ESPKnockOff.Data;
using Microsoft.AspNetCore.Authorization;

namespace ESPKnockOff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Municipalities : Controller
    {
        private readonly DatabaseService _dbService;

        public Municipalities(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Municipality>>> GetMunicipalities()
        {
            var municipalities = await _dbService.GetObjects<Municipality>();

            int ETag = 0;
            foreach (var municipality in municipalities)
            {
                ETag += municipality.GetHashCode();
            }

            if (Request.Headers["ETag"] != ETag.ToString())
            {
                Response.Headers.Add("ETag", ETag.ToString());
                return municipalities;
            }
            else
            {
                return StatusCode(304);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Municipality>> GetMunicipality(int id)
        {
            var municipality = await _dbService.GetObjectById<Municipality>(id);

            if (municipality == null)
            {
                return NotFound();
            }
            else if (Request.Headers["ETag"] != municipality.GetHashCode().ToString())
            {
                Response.Headers.Add("ETag", municipality.GetHashCode().ToString());
                return Ok(municipality);
            }
            else
            {
                return StatusCode(304);
            }
        }

        [HttpGet("{id}/suburbs")]
        public async Task<ActionResult<List<Suburb>>> GetMunicipalitySuburbs(int id)
        {
            try
            {
                var suburbs = await _dbService.GetObjectSubObjects<Municipality, Suburb>(id);

                int etag = 0;
                foreach (var suburb in suburbs)
                {
                    etag += suburb.GetHashCode();
                }

                if (Request.Headers["ETag"] != etag.ToString())
                {
                    Response.Headers.Add("ETag", etag.ToString());
                    return suburbs;
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
        public async Task<ActionResult> AddMunicipality(Municipality municipality)
        {
            try
            {
                var result = _dbService.Insert(municipality);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateMunicipality(Municipality municipality)
        {
            try
            {
                var result = _dbService.Update(municipality);
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
