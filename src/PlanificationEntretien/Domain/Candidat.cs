using System;

namespace PlanificationEntretien.Domain
{
    public record Candidat(Guid Id, string Language, string Email, int? Xp);
}