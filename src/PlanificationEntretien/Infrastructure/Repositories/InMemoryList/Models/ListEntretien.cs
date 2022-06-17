using System;

namespace PlanificationEntretien.Infrastructure.Repositories.InMemory.Models
{
    public record ListEntretien(Guid Id, DateTime DateEtHeure, string EmailCandidat, string EmailRecruteur);
}