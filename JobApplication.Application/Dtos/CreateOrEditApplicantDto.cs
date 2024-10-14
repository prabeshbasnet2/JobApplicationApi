using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Application.Dtos
{
    public class CreateOrEditApplicantDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "FirstName is required.")]
        [MaxLength(100)]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(100)]
        public required string LastName { get; set; }

        [Phone]
        [RegularExpression(@"^\+\d{1,3}\d{1,14}$", ErrorMessage = "Phone number must be in international format, e.g., +1234567890.")]
        public required string Phone { get; set; }

        [MaxLength(100)]
        [RegularExpression(@"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]-(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$",
        ErrorMessage = "Time interval must be in HH:mm-HH:mm format.")]
        public string? BestCallTime { get; set; }

        [MaxLength(100)]
        public string? LinkedInUrl { get; set; }

        [MaxLength(100)]
        public string? GitHubUrl { get; set; }

        [Required(ErrorMessage = "Comments is required.")]
        [MaxLength(1000)]
        public required string Comments { get; set; }
    }
}
