using System;

namespace PlanificationEntretien.Models
{
    public record Candidat(Guid Id, string Language, string Email, int? Xp);
}