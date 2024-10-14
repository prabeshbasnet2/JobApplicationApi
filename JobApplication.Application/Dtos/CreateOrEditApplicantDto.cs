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
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public required string LastName { get; set; }

        [Phone]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Phone number must be in international format, e.g., +1234567890.")]
        public required string Phone { get; set; }

        [MaxLength(100)]
        [RegularExpression(@"^\d{2}:\d{2}-\d{2}:\d{2}$", ErrorMessage = "Time interval must be in HH:mm-HH:mm format")]
        public string? BestCallTime { get; set; }

        [MaxLength(100)]
        public string? LinkedInUrl { get; set; }

        [MaxLength(100)]
        public string? GitHubUrl { get; set; }

        [Required]
        [MaxLength(1000)]
        public required string Comments { get; set; }
    }
}
