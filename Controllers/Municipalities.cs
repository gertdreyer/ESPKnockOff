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
    public class Municipalities : Controller
    {
        private readonly ApplicationContext _context;

        public Municipalities(ApplicationContext context)
        {
            _context = context;
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
    }
}
