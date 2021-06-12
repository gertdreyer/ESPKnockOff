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
    public class Suburbs : Controller
    {
        private readonly ApplicationContext _context;
        private readonly InsertService _insertService;

        public Suburbs(ApplicationContext context, InsertService insertService)
        {
            _context = context;
            _insertService = insertService;
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

        [HttpPost]
        public async Task<ActionResult> AddSuburb(Suburb suburb)
        {
            try
            {
                _insertService.Insert(suburb);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
