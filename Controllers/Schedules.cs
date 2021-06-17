using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESPKnockOff.Models;
using ESPKnockOff.Services;
using System.Net.Http;
using ESPKnockOff.Models.Enums;
using ESPKnockOff.Data;
using Microsoft.AspNetCore.Authorization;
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
                return await _dbService.GetObjects<Schedule>(new FilteringCoditions()
                {
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetSchedule(int id)
        {
            Schedule schedule = await _dbService.GetObjectById<Schedule>(id);

            if (schedule == null)
            {
                return NotFound();
            }

            return schedule;
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
                return Ok(result);
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
