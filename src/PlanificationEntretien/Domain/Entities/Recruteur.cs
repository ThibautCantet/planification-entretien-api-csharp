using System;

namespace PlanificationEntretien.Domain.Entities
{
    public record Recruteur(Guid Id, string Language, string Email, int? Xp);
}