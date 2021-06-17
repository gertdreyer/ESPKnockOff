﻿using Microsoft.AspNetCore.Mvc;
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
	public class Provinces : Controller {
		private readonly ApplicationContext _context;
		private readonly InsertService _insertService;

		public Provinces(ApplicationContext context, InsertService insertService) {
			_context = context;
			_insertService = insertService;
		}

		[HttpGet]
		public List<Province> GetProvinces() {
			return _context.Province.ToList();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Province>> GetProvince(int id) {
			var province = await _context.Province.FindAsync(id);

			if (province == null) {
				return NotFound();
			}

			return province;
		}

		[HttpGet("{id}/municipalities")]
		public async Task<ActionResult<List<Municipalities>>> GetProvinceMunicipalities(int id) {
			// TODO: Get municipalities in province.
			var municipalities = new List<Municipalities>();
			return municipalities;
		}

		[HttpPost]
		public async Task<ActionResult> AddProvince(Province province) {
			try {
				_insertService.Insert(province);
				return Ok();
			} catch (Exception e) {
				return BadRequest(e.ToString());
			}
		}
	}
}
