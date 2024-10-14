using JobApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Infrastructure
{
    public class ApplicantDbContext : DbContext
    {
        public ApplicantDbContext(DbContextOptions<ApplicantDbContext> options)
            : base(options) { }

        public DbSet<Applicant> Applicants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Applicant>(entity =>
            {
                entity.HasKey(s => s.Email);
                entity.Property(s => s.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(s => s.LastName).IsRequired().HasMaxLength(100);
                entity.Property(s => s.Phone);
                entity.Property(s => s.BestCallTime);
                entity.Property(s => s.GitHubUrl);
                entity.Property(s => s.LinkedInUrl);
                entity.Property(s => s.Comments).IsRequired();
                entity.HasIndex(s => s.Email).IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
