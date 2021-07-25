using Microsoft.AspNetCore.Authorization;
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
    [Route("PD")]
    public class PersonDepartmentsController : ControllerBase
    {

        private readonly ILogger<PersonDepartmentsController> _logger;
        private readonly IPersonDepartmentsRepository _repository;

        public PersonDepartmentsController(IPersonDepartmentsRepository repository, ILogger<PersonDepartmentsController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Add([FromBody] IReadOnlyList<PersonDepartment> request)
        {
            await _repository.AddAsync(request);

            _logger.LogInformation("Register PD(s) to DB");

            return Ok();
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {

            var response = await _repository.GetByIdAsync(id);

            _logger.LogInformation("Get PD by Id from DB");

            return Ok(response);
        }

        [HttpGet("getPagination")]
        public async Task<IActionResult> GetWithPagination([FromQuery] int skip, int take, string search)
        {

            var response = await _repository.GetWithPaginationAsync(skip, take, search);

            _logger.LogInformation("Get PD with pagination");

            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] IReadOnlyList<PersonDepartment> request)
        {
            await _repository.UpdateAsync(request);

            _logger.LogInformation("Updated PD(s) in DB");

            return Ok();
        }

        [HttpDelete("unregister/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _repository.DeleteAsync(id);

            _logger.LogInformation("Unregister PD By Id");

            return Ok();
        }


    }
}
