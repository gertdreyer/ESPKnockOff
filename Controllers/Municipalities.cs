using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESPKnockOff.Models;
using ESPKnockOff.Services;
using ESPKnockOff.Data;
using Microsoft.AspNetCore.Authorization;

namespace ESPKnockOff.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class Municipalities : Controller {
		private readonly ApplicationContext _context;
		private readonly DatabaseService _dbService;

		public Municipalities(ApplicationContext context, DatabaseService dbService) {
			_context = context;
			_dbService = dbService;
		}

		[HttpGet]
		public List<Municipality> GetMunicipalities() {
			return _context.Municipality.ToList();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Municipality>> GetMunicipality(int id) {
			var municipality = await _context.Municipality.FindAsync(id);

			if (municipality == null) {
				return NotFound();
			}

			return municipality;
		}

		[HttpGet("{id}/suburbs")]
		public async Task<ActionResult<List<Suburb>>> GetMunicipalitySuburbs(int id) {
			// TODO: Get suburbs in municipality.
			var suburbs = new List<Suburb>();
			return suburbs;
		}

		[HttpPost]
		public async Task<ActionResult> AddMunicipality(Municipality municipality) {
			try {
				_dbService.Insert(municipality);
				return Ok();
			} catch (Exception e) {
				return BadRequest(e.ToString());
			}
		}

		[HttpPut]
		public async Task<ActionResult> UpdateMunicipality(Municipality municipality) {
			try {
				_dbService.Update(municipality);
				return Ok();
			} catch (Exception e) {
				return BadRequest(e.ToString());
			}
		}

		[HttpDelete]
		public async Task<ActionResult> RemoveMunicipality(Municipality municipality) {
			try {
				_dbService.Remove(municipality);
				return Ok();
			} catch (Exception e) {
				return BadRequest(e.ToString());
			}
		}
	}
}
