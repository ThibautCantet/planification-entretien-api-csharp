using System;

namespace PlanificationEntretien.Domain.Entities
{
    public record Candidat(Guid Id, string Language, string Email, int? Xp);
}