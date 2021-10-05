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
    public class PersonController : ControllerBase
    {
        
        private readonly ILogger<PersonController> _logger;

        private readonly IPersonRepository _repository;

        private readonly IPersonValidationService _validationService;

        public PersonController(IPersonRepository repository, ILogger<PersonController> logger, IPersonValidationService validationService)
        {
            _logger = logger;

            _repository = repository;

            _validationService = validationService;
        }

        [HttpPost("persons")]
        public async Task<IActionResult> Add([FromBody] IReadOnlyList<PersonRequest> request)
        {
            IReadOnlyList<IOperationFailure> failures = new List<IOperationFailure>();

            foreach (PersonRequest r in request)
            {
                failures = _validationService.ValidateEntity(new PersonRequest() { FirstName = r.FirstName, LastName = r.LastName, Email =r.Email, Age = r.Age });
            }

            if (failures.Count > 0)
            {
                return BadRequest(failures);
            }

            await _repository.AddAsync(request);
                
            _logger.LogInformation("Added Person(s) to DB");

            return Ok();
        }

        [HttpGet("personsbyid")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            
            var response = await _repository.GetByIdAsync(id);

            _logger.LogInformation("Get Person by Id from DB");

            return Ok(response);
        }

        [HttpGet("personsbyname")]
        public async Task<IActionResult> GetByName([FromQuery] string name )
        {

            var response = await _repository.GetByNameAsync(name);

            _logger.LogInformation("Get Person by name from DB");

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
        public async Task<IActionResult> Update([FromBody] IReadOnlyList<PersonRequest> request)
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
