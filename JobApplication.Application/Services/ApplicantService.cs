﻿using JobApplication.Application.Dtos;
using JobApplication.Application.Interfaces;
using JobApplication.Domain.Entities;
using JobApplication.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Application.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly IApplicantRepository _applicantRepository;

        public ApplicantService(IApplicantRepository applicantRepository)
        {
            _applicantRepository = applicantRepository;
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
