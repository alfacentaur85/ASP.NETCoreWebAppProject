using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.Extensions.Logging;
using datalayer.Interfaces;
using Secutrity;
using ValidationService.Interfaces;
using datalayer.Responses;
using System.Collections.Generic;


namespace CourseProject.Controllers
{
    [ApiController]
    public sealed class SecurityController : ControllerBase
    {
        private readonly ISecurityService _userService;

        private readonly ILogger<SecurityController> _logger;

        private readonly ISecurityRepository _repository;

        private readonly ISecurityValidationService _validationService;

        public SecurityController(ISecurityService userService, ISecurityRepository repository, ILogger<SecurityController> logger, ISecurityValidationService validationService)
        {
            _userService = userService;

            _logger = logger;

            _repository = repository;

            _validationService = validationService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromQuery] string user, string password)
        {
            IList<LoginPassword> loginpassword = new List<LoginPassword>();

            loginpassword.Add(new LoginPassword() { Login = user, Password = password });

            IReadOnlyList<IOperationFailure> failures = new List<IOperationFailure>();

            foreach (LoginPassword r in loginpassword)
            {
                failures = _validationService.ValidateEntity(r);
            }

            if (failures.Count > 0)
            {
                return BadRequest(failures);
            }

            TokenResponse token = _userService.Authenticate(user, password);
            if (token is null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            SetTokenCookie(token.RefreshToken);
            return Ok(token);
        }

        [Authorize]
        [HttpPost("refresh-token")]
        public IActionResult Refresh()
        {
            string oldRefreshToken = Request.Cookies["refreshToken"];

            string newRefreshToken = _userService.RefreshToken(oldRefreshToken);

            if (string.IsNullOrWhiteSpace(newRefreshToken))
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            SetTokenCookie(newRefreshToken);

            return Ok(newRefreshToken);
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
    }
}
	

    