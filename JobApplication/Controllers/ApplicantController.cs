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

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllApplicants()
        {
            try
            {
                // Call the service to get all applicants
                var applicants = await _applicantService.GetApplicants(); 

                // Check if applicants are found
                if (applicants == null || !applicants.Any())
                {
                    return NotFound(new
                    {
                        Message = "No applicants found."
                    });
                }

                return Ok(applicants); // Return the list of applicants
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

