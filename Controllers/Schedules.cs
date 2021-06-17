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

namespace ESPKnockOff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class Schedules : Controller
    {
        private readonly ApplicationContext _context;
        private readonly DatabaseService _dbService;

        public Schedules(ApplicationContext context, DatabaseService dbService)
        {
            _context = context;
            _dbService = dbService;
        }

        [HttpGet]
        public ActionResult GetSchedules(int stage, int day, int startTime, int endTime)
        {
            // TODO Get and return all schedules based on the query paramaters.
            return Ok(new
            {
                id = 1,
                day = day,
                stage = stage,
                startTime = startTime,
                endTime = endTime,
            });
        }

        [HttpGet("id")]
        public async Task<ActionResult<Schedule>> GetSchedule(int id)
        {
            Schedule schedule = await _dbService.GetObjectById<Schedule>(id);

            if (schedule == null)
            {
                return NotFound();
            }

            return schedule;
        }

        [HttpPost]
        public async Task<ActionResult> AddSchedule(Schedule schedule)
        {
            try
            {
                _dbService.Insert(schedule);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSchedule(Schedule schedule)
        {
            try
            {
                _dbService.Update(schedule);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

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
