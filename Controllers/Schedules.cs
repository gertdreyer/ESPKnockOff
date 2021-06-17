using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ESPKnockOff.Models;
using ESPKnockOff.Services;
using System.Net.Http;
using ESPKnockOff.Models.Enums;
using ESPKnockOff.Services.Getters;

namespace ESPKnockOff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Schedules : Controller
    {
        private readonly DatabaseService _dbService;

        public Schedules(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Schedule>>> GetSchedules(int stage, int day, string fromTime, string toTime)
        {
            try
            {
                var schedules = await _dbService.GetObjects<Schedule>(new FilteringCoditions()
                {
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetSchedule(int id)
        {
            Schedule schedule = await _dbService.GetObjectById<Schedule>(id);
            var scheduleETag = schedule.GetHashCode().ToString();

            if (schedule == null)
            {
                return NotFound();
            }
            else if (Request.Headers["ETag"] != scheduleETag)
            {
                Response.Headers.Add("ETag", scheduleETag);
                return schedule;
            }
            else
            {
                return StatusCode(304);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddSchedule(Schedule schedule)
        {
            try
            {
                var result = _dbService.Insert(schedule);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateSchedule(Schedule schedule)
        {
            try
            {
                var result = _dbService.Update(schedule);

                if (Request.Headers["ETag"] != result.GetHashCode().ToString())
                {
                    Response.Headers.Add("ETag", result.GetHashCode().ToString());
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

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> RemoveSchedule(Schedule schedule)
        {
            try
            {
                _dbService.Remove(schedule);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpGet("stage")]
        public async Task<ActionResult> GetEskomStatus()
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.ParseAdd("text/html");
                var httpres = await httpClient.GetAsync("https://loadshedding.eskom.co.za/LoadShedding/GetStatus");
                var rawloadsheddingStatus = int.Parse(await httpres.Content.ReadAsStringAsync());
                var loadsheddingStatusEnum = (LoadsheddingStatus)(rawloadsheddingStatus != -1 ? rawloadsheddingStatus - 1 : -1);
                return Ok(loadsheddingStatusEnum);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
