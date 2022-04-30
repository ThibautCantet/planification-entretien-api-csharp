using System;

namespace PlanificationEntretien.Infrastructure.Repositories.InMemory.Models
{
    public record InMemoryEntretien(Guid Id, DateTime DateEtHeure, string EmailCandidat, string EmailRecruteur);
}