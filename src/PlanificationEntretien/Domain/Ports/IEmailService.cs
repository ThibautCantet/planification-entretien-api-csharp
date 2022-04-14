namespace PlanificationEntretien.Domain.Ports
{
    public interface IEmailService
    {
        void SendToCandidat(string email);
        void SendToRecruteur(string email);
    }
}