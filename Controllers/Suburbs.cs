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
    public class Suburbs : Controller
    {
        private readonly ApplicationContext _context;

        public Suburbs(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Suburb> GetSuburbs()
        {
            return _context.Suburb.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Suburb>> GetSuburb(int id)
        {
            var suburb = await _context.Suburb.FindAsync(id);

            if (suburb == null)
            {
                return NotFound();
            }

            return suburb;
        }

        [HttpGet("{id}/schedules")]
        public async Task<ActionResult> GetSuburbSchedules(int id, int stage, int day, int startTime, int endTime)
        {
            // TODO: Get and returl all the schedules in a suburb based on the query paramaters.
            return Ok(new {
                id = 1,
                day = day,
                stage = stage,
                startTime = startTime,
                endTime = endTime,
            });
        }
    }
}
