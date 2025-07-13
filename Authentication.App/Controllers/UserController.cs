using Authentication.Domain.DTOs;
using Authentication.Domain.Services;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        [Route("getUser")]
        public IActionResult GetUser()
        {
            return Ok("User is authenticated");
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
                var token = await _userService.UserLoginAsync(login.userName,login.Password);
                if(string.IsNullOrEmpty(token))
                {
                    return Unauthorized("Invalid UserName or password.");
                }
                return Ok(token);
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
        public IActionResult GetReportTemplates(
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

    }
}
