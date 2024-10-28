namespace JobApplication.Application.Dtos
{
    public class ApplicantDto
    {
        public required string Email { get; set; }
        public required string Name { get; set; }
        public string? Phone { get; set; }
        public string? BestCallTime { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? GitHubUrl { get; set; }
        public required string Comments { get; set; }
    }
}
