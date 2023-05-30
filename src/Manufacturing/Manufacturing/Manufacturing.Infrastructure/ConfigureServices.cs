using Manufacturing.Application.Common.Interfaces;
using Manufacturing.Infrastructure.Persistence;
using Manufacturing.Infrastructure.Persistence.Interceptors;
using Manufacturing.Infrastructure.Persistence.Repositories;
using Manufacturing.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Manufacturing.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<ApplicationDbContextInitialiser>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IDateTime, DateTimeService>();

            return services;
        }
    }
}
