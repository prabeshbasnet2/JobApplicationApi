using JobApplication.Domain.Entities;
using JobApplication.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Infrastructure.Repositories
{
    public class ApplicantRepository : IApplicantRepository
    {
        private readonly ApplicantDbContext _dbContext;

        public ApplicantRepository(ApplicantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Applicant applicant)
        {
            await _dbContext.Applicants.AddAsync(applicant);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string email)
        {
            var applicant = await GetByEmailAsync(email);
            if (applicant != null)
            {
                _dbContext.Applicants.Remove(applicant);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Applicant>> GetAllAsync()
        {
            return await _dbContext.Applicants.ToListAsync();
        }

        public async Task<Applicant> GetByEmailAsync(string email)
        {
            return await _dbContext.Applicants.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task UpdateAsync(Applicant applicant)
        {
            _dbContext.Applicants.Update(applicant);
            await _dbContext.SaveChangesAsync();
        }
    }
}
