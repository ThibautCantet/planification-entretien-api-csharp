using Microsoft.Extensions.DependencyInjection;
using PlanificationEntretien.Infrastructure.Repositories;
using PlanificationEntretien.UserCase;

namespace PlanificationEntretien.Infrastructure
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddSingleton<IPlanifierEntretien, PlanifierEntretien>();
            services.AddSingleton<IListerEntretiens, ListerEntretiens>();
            services.AddSingleton<IEntretienRepository, EntretienRepository>();
            services.AddSingleton<ICandidatRepository, CandidatRepository>();
            services.AddSingleton<IRecruteurRepository, RecruteurRepository>();
            services.AddSingleton<IEmailService, DummyEmailService>();

            return services;
        }
    }
}