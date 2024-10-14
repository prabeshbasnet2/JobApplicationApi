using JobApplication.Application.Dtos;
using JobApplication.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicantController : ControllerBase
    {
        private readonly IApplicantService _applicantService;

        public ApplicantController(IApplicantService applicantService)
        {
            _applicantService = applicantService;
        }

        [HttpPost("createOrEdit")]
        public async Task<IActionResult> CreateOrEditApplicant([FromBody] CreateOrEditApplicantDto applicantDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _applicantService.CreateOrEditApplicant(applicantDto);
                return Ok(new
                {
                    Message = "Applicant has been created or updated successfully."
                });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message
                });
            }
            catch (Exception ex)
            {
                // Log the exception (e.g., using a logging framework like Serilog)
                return StatusCode(500, new
                {
                    Error = "An unexpected error occurred while processing your request.",
                    Details = ex.Message 
                });
            }
        }
    }
}

