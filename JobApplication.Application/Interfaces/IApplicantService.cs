using JobApplication.Application.Dtos;

namespace JobApplication.Application.Interfaces
{
    public interface IApplicantService
    {
        Task CreateOrEditApplicant(CreateOrEditApplicantDto applicantDto);

        Task<List<ApplicantDto>> GetApplicants();

    }
}
