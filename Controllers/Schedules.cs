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
    public class Schedules : Controller
    {
        private readonly ApplicationContext _context;
        private readonly InsertService _insertService;

        public Schedules(ApplicationContext context)
        {
            _context = context;
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
        public ActionResult GetSchedule(int id)
        {
            // TODO Get and return the schedule for a given id.
            return Ok();
        }
    }
}
