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
    public class Municipalities : Controller
    {
        private readonly ApplicationContext _context;
        private readonly InsertService _insertService;

        public Municipalities(ApplicationContext context, InsertService insertService)
        {
            _context = context;
            _insertService = insertService;
        }

        [HttpGet]
        public List<Municipality> GetMunicipalities()
        {
            return _context.Municipality.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Municipality>> GetMunicipality(int id)
        {
            var municipality = await _context.Municipality.FindAsync(id);

            if (municipality == null)
            {
                return NotFound();
            }

            return municipality;
        }

        [HttpPost]
        public async Task<ActionResult> AddMunicipality(Municipality municipality)
        {
            try
            {
                _insertService.Insert(municipality);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
