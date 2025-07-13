using Authentication.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Authentication.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly IUserService _userService;
        public PatientController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("getPatient")]  
        public IActionResult GetPatientData()
        {
            try
            {
                var userName = User.Identity?.Name;
                var expires = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Expiration)?.Value;

                return Ok(userName + " " + expires);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Policy = "User1OrUser4")]
        [HttpGet("assignPatients")]
        public IActionResult AssignPatients()
        {
            return Ok("Patients assigned successfully.");
        }

        [Authorize(Policy = "User1OrUser2OrUser3OrUser4")]
        [HttpGet("messagePatients")]
        public IActionResult MessagePatients()
        {
            return Ok("Patients message successfully.");
        }

        [Authorize(Policy = "User2OrUser4")]
        [HttpGet("emergencyPatients")]
        public IActionResult EmergencyPatient()
        {
            return Ok("Emergency patients assigned successfully.");
        }
    }
}
