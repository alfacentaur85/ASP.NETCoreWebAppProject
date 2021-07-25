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
    public class DepartmentController : ControllerBase
    {

        private readonly ILogger<DepartmentController> _logger;
        private readonly IDepartmentRepository _repository;

        public DepartmentController(IDepartmentRepository repository, ILogger<DepartmentController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost("departments")]
        public async Task<IActionResult> Add([FromBody] IReadOnlyList<Department> request)
        {
            await _repository.AddAsync(request);

            _logger.LogInformation("Added Department(s) to DB");

            return Ok();
        }

        [HttpGet("departments/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {

            var response = await _repository.GetByIdAsync(id);

            _logger.LogInformation("Get Department by Id from DB");

            return Ok(response);
        }

        [HttpGet("departments")]
        public async Task<IActionResult> GetWithPagination([FromQuery] int skip, int take, string search)
        {

            var response = await _repository.GetWithPaginationAsync(skip, take, search);

            _logger.LogInformation("Get Department with pagination");

            return Ok(response);
        }

        [HttpPut("departments")]
        public async Task<IActionResult> Update([FromBody] IReadOnlyList<Department> request)
        {
            await _repository.UpdateAsync(request);

            _logger.LogInformation("Updated Department(s) in DB");

            return Ok();
        }

        [HttpDelete("departments/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _repository.DeleteAsync(id);

            _logger.LogInformation("Deleted Department By Id");

            return Ok();
        }


    }
}
