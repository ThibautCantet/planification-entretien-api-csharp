using System;

namespace PlanificationEntretien.Infrastructure.Repositories.InMemory.Models
{
    public record InMemoryCandidat(Guid Id, string Language, string Email, int? Xp);
}