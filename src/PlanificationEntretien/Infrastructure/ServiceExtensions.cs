using Microsoft.Extensions.DependencyInjection;
using PlanificationEntretien.Domain.Ports;
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
            services.AddSingleton<ICreerCandidat, CreerCandidat>();
            services.AddSingleton<ICreerRecruteur, CreerRecruteur>();
            services.AddSingleton<IListerRecruteursExperimentes, ListerRecruteursExperimentes>();
            services.AddSingleton<IEntretienRepository, ListEntretienRepository>();
            services.AddSingleton<ICandidatRepository, ListCandidatRepository>();
            services.AddSingleton<IRecruteurRepository, ListRecruteurRepository>();
            services.AddSingleton<IEmailService, DummyEmailService>();

            return services;
        }
    }
}