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
    public class Schedules : Controller
    {
        private readonly ApplicationContext _context;

        public Schedules(ApplicationContext context)
        {
            _context = context;
        }
    }
}
