using JobApplication.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Application.Interfaces
{
    public interface IApplicantService
    {
        Task CreateOrEditApplicant(CreateOrEditApplicantDto applicantDto);
    }
}
