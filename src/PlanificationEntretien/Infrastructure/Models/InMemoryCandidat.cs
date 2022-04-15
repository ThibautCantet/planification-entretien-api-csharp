using System;

namespace PlanificationEntretien.Infrastructure.Models
{
    public record InMemoryCandidat(Guid Id, string Language, string Email, int? Xp);
}