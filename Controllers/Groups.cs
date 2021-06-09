using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESPKnockOff.Models;

namespace ESPKnockOff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Groups : Controller
    {
        private readonly ApplicationContext _context;

        public Groups(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult GetGroups()
        {
            // TODO: Get and return all the groups
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult GetGroup(int id)
        {
            // TODO: Get and return the group for the given id
            return Ok();
        }

        [HttpGet("{id}/schedules")]
        public async Task<ActionResult> GetGroupSchedules(int id, int stage, int day, int startTime, int endTime)
        {
            // TODO: Get and returl all the schedules in a group based on the query paramaters.
            return Ok(new
            {
                id = 1,
                day = day,
                stage = stage,
                startTime = startTime,
                endTime = endTime,
            });
        }
    }
}
