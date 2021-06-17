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
        public async Task<ActionResult<List<Suburb>>> GetSuburbs()
        {
            var suburbs = await _dbService.GetObjects<Suburb>();

            int ETag = 0;
            foreach (var suburb in suburbs)
            {
                ETag += suburb.GetHashCode();
            }

            if (Request.Headers["ETag"] != ETag.ToString())
            {
                Response.Headers.Add("ETag", ETag.ToString());
                return suburbs;
            }
            else
            {
                return StatusCode(304);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Suburb>> GetSuburb(int id)
        {
            var suburb = await _dbService.GetObjectById<Suburb>(id);

            if (suburb == null)
            {
                return NotFound();
            }
            else if (Request.Headers["ETag"] != suburb.GetHashCode().ToString())
            {
                Response.Headers.Add("ETag", suburb.GetHashCode().ToString());
                return suburb;
            }
            else
            {
                return StatusCode(304);
            }
        }

        [HttpGet("{id}/schedules")]
        public async Task<ActionResult<List<Schedule>>> GetSuburbSchedules(int id, int stage, int day, string fromTime, string toTime)
        {
            try
            {
                var schedules = await _dbService.GetObjectSubObjects<Suburb, Schedule>(id, new FilteringCoditions() {
                    Day = day,
                    Stage = stage,
                    FromTime = fromTime,
                    ToTime = toTime
                });

                int ETag = 0;
                foreach (var schedule in schedules)
                {
                    ETag += schedule.GetHashCode();
                }

                if (Request.Headers["ETag"] != ETag.ToString())
                {
                    Response.Headers.Add("ETag", ETag.ToString());
                    return schedules;
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
