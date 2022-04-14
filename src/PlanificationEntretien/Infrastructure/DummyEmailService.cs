using PlanificationEntretien.Domain;
using PlanificationEntretien.UserCase;

namespace PlanificationEntretien.Infrastructure
{
    public class DummyEmailService : IEmailService
    {
        public void SendToCandidat(string email)
        {
        }

        public void SendToRecruteur(string email)
        {
        }
    }
}