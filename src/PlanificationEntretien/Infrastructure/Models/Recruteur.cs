using System;

namespace PlanificationEntretien.Infrastructure.Models
{
    public record Recruteur(Guid Id, string Language, string Email, int? Xp);
}