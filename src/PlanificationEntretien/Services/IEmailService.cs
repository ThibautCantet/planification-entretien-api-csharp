namespace PlanificationEntretien.Services
{
    public interface IEmailService
    {
        void SendToCandidat(string email);
        void SendToRecruteur(string email);
    }
}