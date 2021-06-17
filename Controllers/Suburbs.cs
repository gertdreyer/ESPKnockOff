﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESPKnockOff.Models;
using ESPKnockOff.Services.Getters;
using ESPKnockOff.Services;

namespace ESPKnockOff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Suburbs : Controller
    {
        private readonly DatabaseService _dbService;

        public Suburbs(ApplicationContext context, DatabaseService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<List<Suburb>> GetSuburbs()
        {
            return await _dbService.GetObjects<Suburb>();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Suburb>> GetSuburb(int id)
        {
            var suburb = await _dbService.GetObjectById<Suburb>(id);

            if (suburb == null)
            {
                return NotFound();
            }

            return suburb;
        }

        [HttpGet("{id}/schedules")]
        public async Task<ActionResult<List<Schedule>>> GetSuburbSchedules(int id, int stage, int day, string startTime, string endTime)
        {
            return await _dbService.GetObjectSubObjects<Suburb, Schedule>(id, new FilteringCoditions() { Day = day, Stage = stage, StartTime = startTime, Endtime = endTime });
        }

        [HttpPost]
        public async Task<ActionResult> AddSuburb(Suburb suburb)
        {
            try
            {
                _dbService.Insert(suburb);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSuburb(Suburb suburb)
        {
            try
            {
                _dbService.Update(suburb);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveSuburb(Suburb suburb)
        {
            try
            {
                _dbService.Remove(suburb);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}
