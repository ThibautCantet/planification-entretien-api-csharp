namespace PlanificationEntretien.Domain
{
    public interface IEmailService
    {
        void SendToCandidat(string email);
        void SendToRecruteur(string email);
    }
}