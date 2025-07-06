using Authentication.Domain.DTOs;
using Authentication.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

namespace Authentication.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IWordDocumentRead _documentRead;
        public UserController(IUserService userService, IWordDocumentRead documentRead)
        {
            _userService = userService;
            _documentRead = documentRead;
        }
        [HttpPost]
        [Route("userRegistration")]
        public async Task<IActionResult> RegistrationAsync([FromBody] RegistrationDto registrationDto)
        {
            
            if (registrationDto == null)
            {
                return BadRequest("Registration data cnanot be empty.");
            }
            try
            {
                var result = await _userService.UserRegistrationAsync(registrationDto);
                if (string.IsNullOrEmpty(result))
                {
                    return StatusCode(500, "User registration failed.");
                }
                return Ok(new { UserId = result });
            }
            catch (Exception ex)
            {
               return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("userLogin")]
        public async Task<IActionResult> UserLogin([FromBody] LoginDto login)
        {
            if (login == null) 
            {
                return BadRequest("Login data cant be empty");
            }
            try
            {
                var token = await _userService.UserLoginAsync(login.Email,login.Password);
                if(string.IsNullOrEmpty(token))
                {
                    return Unauthorized("Invalid email or password.");
                }
                return Ok(new { Token = token });
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("getData")]
        public async Task<IActionResult> GetRegistrationData()
        {
            
            try
            {
                var res = await _userService.GetRegistrationDataAsync();
                if (res == null)
                {
                    return NoContent();
                }
                return Ok(res);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("getTemplates")]
        public async Task<IActionResult> GetReportTemplates(
            [FromQuery] string doctorId,
            [FromQuery] string modality,
            [FromQuery] string fileName
        )
        {
            if (string.IsNullOrWhiteSpace(doctorId) ||
                string.IsNullOrWhiteSpace(modality) ||
                string.IsNullOrWhiteSpace(fileName))
            {
                return BadRequest("doctorId, modality, and fileName are required.");
            }

            try
            {
                string template = _documentRead.GetDocumentHtml(doctorId, modality, fileName);
                if (string.IsNullOrEmpty(template))
                {
                    return NotFound("Template not found.");
                }

                return Content(template, "text/html", Encoding.UTF8);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + " Templates not derived");
            }
        }
        [HttpGet("getPatient")]
        [Authorize]
        public async Task<IActionResult> GetPatientData()
        {
            try
            {
                var userName = User.Identity?.Name;
                var expires = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Expiration)?.Value;

                return Ok(userName+" "+ expires);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
