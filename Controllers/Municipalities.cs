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
    public class Municipalities : Controller
    {
        private readonly DatabaseService _dbService;

        public Municipalities(ApplicationContext context, DatabaseService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<List<Municipality>> GetMunicipalities()
        {
            return await _dbService.GetObjects<Municipality>();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Municipality>> GetMunicipality(int id)
        {
            var municipality = await _dbService.GetObjectById<Municipality>(id);

            if (municipality == null)
            {
                return NotFound();
            }

            return municipality;
        }

        [HttpGet("{id}/suburbs")]
        public async Task<ActionResult<List<Suburb>>> GetMunicipalitySuburbs(int id)
        {
            try
            {
                return await _dbService.GetObjectSubObjects<Municipality, Suburb>(id);
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
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
