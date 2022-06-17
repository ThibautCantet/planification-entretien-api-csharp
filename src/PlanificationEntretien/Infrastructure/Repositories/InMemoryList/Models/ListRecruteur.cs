using System;

namespace PlanificationEntretien.Infrastructure.Repositories.InMemoryList.Models
{
    public record ListRecruteur(Guid Id, string Language, string Email, int? Xp);
}