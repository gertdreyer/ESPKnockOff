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
    }
}
