using PlanificationEntretien.Domain;

namespace PlanificationEntretien.UserCase;

public interface ICreerRecruteur
{
    Recruteur Execute(string language, string email, int? xp);
}