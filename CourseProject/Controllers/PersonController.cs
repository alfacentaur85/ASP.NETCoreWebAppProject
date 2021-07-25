﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using datalayer.Interfaces;
using datalayer.Models;

namespace CourseProject.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonRepository _repository;

        public PersonController(IPersonRepository repository, ILogger<PersonController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost("persons")]
        public async Task<IActionResult> Add([FromBody] IReadOnlyList<Person> request)
        {
            await _repository.AddAsync(request);
                
            _logger.LogInformation("Added Person(s) to DB");

            return Ok();
        }

        [HttpGet("persons/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            
            var response = await _repository.GetByIdAsync(id);

            _logger.LogInformation("Get Person by Id from DB");

            return Ok(response);
        }

        [HttpGet("persons")]
        public async Task<IActionResult> GetWithPagination([FromQuery] int skip, int take, string search)
        {

            var response = await _repository.GetWithPaginationAsync(skip, take, search);

            _logger.LogInformation("Get Persons with pagination");

            return Ok(response);
        }

        [HttpPut("persons")]
        public async Task<IActionResult> Update([FromBody] IReadOnlyList<Person> request)
        {
            await _repository.UpdateAsync(request);

            _logger.LogInformation("Updated Person(s) in DB");

            return Ok();
        }

        [HttpDelete("persons/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _repository.DeleteAsync(id);

            _logger.LogInformation("Deleted Person By Id");

            return Ok();
        }


    }
}
