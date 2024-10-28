using JobApplication.Application.Dtos;
using JobApplication.Application.Interfaces;
using JobApplication.Domain.Entities;
using JobApplication.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.DataAnnotations;

namespace JobApplication.Application.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly IApplicantRepository _applicantRepository;
        private readonly IMemoryCache _memoryCache;

        private const string ApplicantsCacheKey = "ApplicantsCacheKey";

        public ApplicantService(IApplicantRepository applicantRepository, IMemoryCache memoryCache)
        {
            _applicantRepository = applicantRepository;
            _memoryCache = memoryCache;
        }

        public async Task CreateOrEditApplicant(CreateOrEditApplicantDto applicantDto)
        {
            ValidateApplicantDto(applicantDto);

            // Check if an applicant already exists with the provided email.
            var existingApplicant = await _applicantRepository.GetByEmailAsync(applicantDto.Email);

            if (existingApplicant != null)
            {
                // Update the existing applicant's details.
                existingApplicant.FirstName = applicantDto.FirstName;
                existingApplicant.LastName = applicantDto.LastName;
                existingApplicant.Phone = applicantDto.Phone;
                existingApplicant.BestCallTime = applicantDto.BestCallTime;
                existingApplicant.GitHubUrl = applicantDto.GitHubUrl;
                existingApplicant.LinkedInUrl = applicantDto.LinkedInUrl;
                existingApplicant.Comments = applicantDto.Comments;

                await _applicantRepository.UpdateAsync(existingApplicant);
            }
            else
            {
                // Create a new applicant.
                var newApplicant = new Applicant
                {
                    Email = applicantDto.Email,
                    FirstName = applicantDto.FirstName,
                    LastName = applicantDto.LastName,
                    Phone = applicantDto.Phone,
                    BestCallTime = applicantDto.BestCallTime,
                    GitHubUrl = applicantDto.GitHubUrl,
                    LinkedInUrl = applicantDto.LinkedInUrl,
                    Comments = applicantDto.Comments,
                };

                await _applicantRepository.AddAsync(newApplicant);
            }

            _memoryCache.Remove(ApplicantsCacheKey);
        }

        public async Task<List<ApplicantDto>> GetApplicants()
        {
            var cachedApplicants = _memoryCache.Get<List<ApplicantDto>>(ApplicantsCacheKey);

            if (cachedApplicants == null)
            {
                // If not in cache, retrieve from the repository.
                var applicants = await _applicantRepository.GetAllAsync();

                // Map domain entities to DTOs.
                cachedApplicants = applicants.Select(a => new ApplicantDto
                {
                    Name = a.FirstName + " " + a.LastName,
                    Email = a.Email,
                    Phone = a.Phone,
                    BestCallTime = a.BestCallTime,
                    GitHubUrl = a.GitHubUrl,
                    LinkedInUrl = a.LinkedInUrl,
                    Comments = a.Comments
                }).ToList();

                _memoryCache.Set(ApplicantsCacheKey, cachedApplicants, TimeSpan.FromMinutes(10));
            }

            return cachedApplicants;
        }


        private void ValidateApplicantDto(CreateOrEditApplicantDto applicantDto)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(applicantDto);
            bool isValid = Validator.TryValidateObject(applicantDto, validationContext, validationResults, true);

            if (!isValid)
            {
                var errors = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
                throw new ValidationException($"Validation failed: {errors}");
            }
        }
    }
}
