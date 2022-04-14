using System;

namespace PlanificationEntretien.Domain
{
    public record Recruteur(Guid Id, string Language, string Email, int? Xp);
}