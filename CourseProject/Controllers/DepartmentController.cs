using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using datalayer.Interfaces;
using datalayer.Requests;
using ValidationService.Interfaces;

namespace CourseProject.Controllers
{
    [ApiController]
    [Authorize]
    [Route("")]
    public class DepartmentController : ControllerBase
    {

        private readonly ILogger<DepartmentController> _logger;

        private readonly IDepartmentRepository _repository;

        private readonly IDepartmentValidationService _validationService;

        public DepartmentController(IDepartmentRepository repository, ILogger<DepartmentController> logger, IDepartmentValidationService validationService)
        {
            _logger = logger;

            _repository = repository;

            _validationService = validationService;
        }

        [HttpPost("departments")]
        public async Task<IActionResult> Add([FromBody] IReadOnlyList<DepartmentRequest> request)
        {
            IReadOnlyList<IOperationFailure> failures = new List<IOperationFailure>();

            foreach (DepartmentRequest r in request)
            {
                failures = _validationService.ValidateEntity(new DepartmentRequest() { Name = r.Name});
            }

            if (failures.Count > 0)
            {
                return BadRequest(failures);
            }

            await _repository.AddAsync(request);

            _logger.LogInformation("Added Department(s) to DB");

            return Ok();
        }

        [HttpGet("departmentsbyid")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {

            var response = await _repository.GetByIdAsync(id);

            _logger.LogInformation("Get Department by Id from DB");

            return Ok(response);
        }

        [HttpGet("departmentsbyname")]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {

            var response = await _repository.GetByNameAsync(name);

            _logger.LogInformation("Get Department by name from DB");

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
        public async Task<IActionResult> Update([FromBody] IReadOnlyList<DepartmentRequest> request)
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
