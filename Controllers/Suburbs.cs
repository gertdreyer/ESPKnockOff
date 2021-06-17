using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ESPKnockOff.Models;
using ESPKnockOff.Services;
using ESPKnockOff.Services.Getters;
using ESPKnockOff.Data;

namespace ESPKnockOff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Suburbs : Controller
    {
        private readonly DatabaseService _dbService;

        public Suburbs(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<List<Suburb>> GetSuburbs()
        {
            return await _dbService.GetObjects<Suburb>();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Suburb>> GetSuburb(int id)
        {
            var suburb = await _dbService.GetObjectById<Suburb>(id);

            if (suburb == null)
            {
                return NotFound();
            }

            return suburb;
        }

        [HttpGet("{id}/schedules")]
        public async Task<ActionResult<List<Schedule>>> GetSuburbSchedules(int id, int stage, int day, string fromTime, string toTime)
        {
            try
            {
                return await _dbService.GetObjectSubObjects<Suburb, Schedule>(id, new FilteringCoditions() {
                    Day = day,
                    Stage = stage,
                    FromTime = fromTime,
                    ToTime = toTime
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddSuburb(Suburb suburb)
        {
            try
            {
                var result = _dbService.Insert(suburb);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateSuburb(Suburb suburb)
        {
            try
            {
                var result = _dbService.Update(suburb);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
