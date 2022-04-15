using PlanificationEntretien.Domain.Entities;

namespace PlanificationEntretien.UserCase;

public interface ICreerCandidat
{
    Candidat Execute(string language, string email, int? xp);
}