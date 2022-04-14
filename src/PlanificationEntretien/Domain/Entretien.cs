using System;

namespace PlanificationEntretien.Domain
{
    public record Entretien(Guid Id, DateTime DateEtHeure, string EmailCandidat, string EmailRecruteur);
}