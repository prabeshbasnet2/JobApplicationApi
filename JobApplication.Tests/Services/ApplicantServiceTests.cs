using JobApplication.Application.Dtos;
using JobApplication.Application.Interfaces;
using JobApplication.Application.Services;
using JobApplication.Domain.Entities;
using JobApplication.Domain.Interfaces;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace JobApplication.Tests.Services
{
    public class ApplicantServiceTests
    {
        private readonly Mock<IApplicantRepository> _applicantRepositoryMock;
        private readonly IApplicantService _applicantService;

        public ApplicantServiceTests()
        {
            _applicantRepositoryMock = new Mock<IApplicantRepository>();
            _applicantService = new ApplicantService(_applicantRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateOrEditApplicant_ShouldCreate_WhenApplicantDoesNotExist()
        {
            // Arrange
            var applicantDto = new CreateOrEditApplicantDto
            {
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Doe",
                Phone = "+1234567890",
                BestCallTime = "10:00-11:00",
                Comments = "Test Comments"
            };

            _applicantRepositoryMock
                .Setup(repo => repo.GetByEmailAsync(applicantDto.Email))
                .ReturnsAsync((Applicant)null);

            await _applicantService.CreateOrEditApplicant(applicantDto);
            _applicantRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Applicant>()), Times.Once);
        }

        [Fact]
        public async Task CreateOrEditApplicant_ShouldUpdate_WhenApplicantExists()
        {
            // Arrange
            var existingApplicant = new Applicant
            {
                Email = "test@example.com",
                FirstName = "Jane",
                LastName = "Doe",
                Phone = "+1234567890",
                BestCallTime = "10:00-11:00",
                Comments = "Old Comments"
            };

            var applicantDto = new CreateOrEditApplicantDto
            {
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Smith",
                Phone = "+1234567890",
                BestCallTime = "12:00-13:00",
                Comments = "Updated Comments"
            };

            _applicantRepositoryMock
                .Setup(repo => repo.GetByEmailAsync(applicantDto.Email))
                .ReturnsAsync(existingApplicant);

            // Act
            await _applicantService.CreateOrEditApplicant(applicantDto);

            // Assert
            _applicantRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<Applicant>(a =>
                a.Email == applicantDto.Email &&
                a.FirstName == applicantDto.FirstName &&
                a.LastName == applicantDto.LastName &&
                a.BestCallTime == applicantDto.BestCallTime &&
                a.Comments == applicantDto.Comments)),
                Times.Once);
        }

        [Theory]
        [InlineData("12", "10:00-11:00")] // Invalid phone number
        [InlineData("+1234567890", "-22:00")] // Invalid time format
        public async Task CreateOrEditApplicant_ShouldThrowValidationException_WhenPhoneOrTimeIsInvalid(string phone, string bestCallTime)
        {
            // Arrange
            var applicantDto = new CreateOrEditApplicantDto
            {
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Doe",
                Phone = phone,
                BestCallTime = bestCallTime,
                Comments = "Test Comments"
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _applicantService.CreateOrEditApplicant(applicantDto));
        }

        [Theory]
        [InlineData(null, "testFirstName", "testLastName", "comments")] // null email
        [InlineData("test@gmail.com", null, "testLastName", "comments")] // null firstName
        [InlineData("test@gmail.com", "testLastName", null, "comments")] // null lastName
        [InlineData("test@gmail.com", "testLastName", "testLastName", null)] // null comments
        public async Task CreateOrEditApplicant_ShouldThrowValidationException_WhenRequiredParamsNull(string email, string firstName, string lastName, string comments)
        {
            // Arrange
            var applicantDto = new CreateOrEditApplicantDto
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Phone = "+9779841413953",
                BestCallTime = "10:00-12:00",
                Comments = comments
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _applicantService.CreateOrEditApplicant(applicantDto));
        }
    }
}
