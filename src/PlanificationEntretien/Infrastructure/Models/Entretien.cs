using System;

namespace PlanificationEntretien.Infrastructure.Models
{
    public record Entretien(Guid Id, DateTime DateEtHeure, string EmailCandidat, string EmailRecruteur);
}