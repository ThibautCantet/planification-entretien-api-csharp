using System;

namespace PlanificationEntretien.Models
{
    public record Recruteur(Guid Id, string Language, string Email, int? Xp);
}