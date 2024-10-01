using API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost("book")]
        public async Task<IActionResult> BookAppointment([FromBody] BookAppointmentRequest request)
        {
            var result = await _appointmentService.BookAppointment(request);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(new { message = "Appointment booked successfully" });
        }

        [HttpGet("user/{userId}/appointments")]
        public async Task<IActionResult> GetAppointments(int userId)
        {
            var appointments = await _appointmentService.GetUserAppointments(userId);
            return Ok(appointments);
        }

        [HttpDelete("cancel/{appointmentId}")]
        public async Task<IActionResult> CancelAppointment(int appointmentId)
        {
            var result = await _appointmentService.CancelAppointment(appointmentId);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(new { message = "Appointment cancelled successfully" });
        }
    }
}
