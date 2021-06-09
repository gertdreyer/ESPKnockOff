using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Controllers
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
    }
}
