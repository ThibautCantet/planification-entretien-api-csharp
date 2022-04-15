using System;

namespace PlanificationEntretien.Infrastructure.Models
{
    public record InMemoryRecruteur(Guid Id, string Language, string Email, int? Xp);
}