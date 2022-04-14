namespace PlanificationEntretien.UserCase
{
    public interface IEmailService
    {
        void SendToCandidat(string email);
        void SendToRecruteur(string email);
    }
}