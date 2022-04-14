using System;

namespace PlanificationEntretien.Domain.Entities
{
    public record Entretien(Guid Id, DateTime DateEtHeure, string EmailCandidat, string EmailRecruteur);
}