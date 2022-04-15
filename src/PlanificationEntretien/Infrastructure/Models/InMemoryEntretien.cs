using System;

namespace PlanificationEntretien.Infrastructure.Models
{
    public record InMemoryEntretien(Guid Id, DateTime DateEtHeure, string EmailCandidat, string EmailRecruteur);
}