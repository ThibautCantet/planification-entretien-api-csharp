using System;

namespace PlanificationEntretien.Domain.Entities
{
    public record Recruteur(Guid Id, string Language, string Email, int? Xp)
    {
        public bool PeutRecruter(Candidat candidat)
        {
            return candidat.Language.Equals(Language)
                && Xp > candidat.Xp;
        }
    }
}