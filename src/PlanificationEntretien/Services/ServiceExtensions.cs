using Microsoft.Extensions.DependencyInjection;
using PlanificationEntretien.Repository;

namespace PlanificationEntretien.Services
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddSingleton<IEntretienService, EntretienService>();
            services.AddSingleton<IEntretienRepository, EntretienRepository>();
            services.AddSingleton<IEmailService, EmailService>();

            return services;
        }
    }
}