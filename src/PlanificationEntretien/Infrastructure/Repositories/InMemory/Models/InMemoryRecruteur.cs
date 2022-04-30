using System;

namespace PlanificationEntretien.Infrastructure.Repositories.InMemory.Models
{
    public record InMemoryRecruteur(Guid Id, string Language, string Email, int? Xp);
}