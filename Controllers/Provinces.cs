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
    public class Provinces : Controller
    {
        private readonly ApplicationContext _context;

        public Provinces(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Province> GetProvinces()
        {
            return _context.Province.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Province>> GetProvince(int id)
        {
            var province = await _context.Province.FindAsync(id);

            if (province == null)
            {
                return NotFound();
            }

            return province;
        }
    }
}
