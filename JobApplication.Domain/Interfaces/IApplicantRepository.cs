using JobApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Domain.Interfaces
{
    public interface IApplicantRepository
    {
        Task<IEnumerable<Applicant>> GetAllAsync();
        Task<Applicant> GetByEmailAsync(string email);
        Task AddAsync(Applicant applicant);
        Task UpdateAsync(Applicant applicant);
        Task DeleteAsync(string email);
    }
}
