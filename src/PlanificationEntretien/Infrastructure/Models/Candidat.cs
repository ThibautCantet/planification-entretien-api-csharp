using System;

namespace PlanificationEntretien.Infrastructure.Models
{
    public record Candidat(Guid Id, string Language, string Email, int? Xp);
}