using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModernisationChallenge.Domain.TaskAggregate;
using ModernisationChallenge.Infrastructure.Repositories;

namespace ModernisationChallenge.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructures(this IServiceCollection services,
                IConfiguration configuration)
        {
            services.AddDbContext<ModernisationDbContext>(
                options => options.UseSqlServer(
                    configuration.GetSection("ModernisationChallenge")["ConntectionString"]));

            // Add Repository
            services.AddScoped<ITaskRepository, TaskRepository>();
            
            return services;
        }
    }
}
