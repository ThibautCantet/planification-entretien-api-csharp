using System;

namespace PlanificationEntretien.Infrastructure.Repositories.InMemory.Models
{
    public record ListCandidat(Guid Id, string Language, string Email, int? Xp);
}