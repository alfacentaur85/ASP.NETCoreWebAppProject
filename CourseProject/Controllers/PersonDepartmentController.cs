using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using datalayer.Interfaces;
using datalayer.Models;
using datalayer.Requests;

namespace CourseProject.Controllers
{
    [ApiController]
    [Authorize]
    [Route("")]
    public class PersonDepartmentController : ControllerBase
    {
        private readonly ILogger<PersonDepartmentController> _logger;

        private readonly IPersonDepartmentRepository _repository;

        public PersonDepartmentController(IPersonDepartmentRepository repository, ILogger<PersonDepartmentController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Add([FromBody] IReadOnlyList<PersonDepartmentRequest> request)
        {
            await _repository.AddAsync(request);

            _logger.LogInformation("Added PersonDepartment(s) to DB");

            return Ok();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] int PersonId, int DepartmentId)
        {
            await _repository.DeleteAsync(PersonId, DepartmentId);

            _logger.LogInformation("Deleted PersonDepartment By Id's");

            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] IReadOnlyList<PersonDepartmentRequest> request)
        {
            await _repository.UpdateAsync(request);

            _logger.LogInformation("Updated PersonDepartment(s) in DB");

            return Ok();
        }


    }
}
