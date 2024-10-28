using JobApplication.Domain.Interfaces;
using JobApplication.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace JobApplication.Infrastructure.DependencyInjection
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IApplicantRepository, ApplicantRepository>();
            return services;
        }
    }
}
