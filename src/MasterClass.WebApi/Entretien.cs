using System;

namespace PlanificationEntretien
{
    public record Entretien(Guid Id, DateTime DateEtHeure, string EmailCandidat, string EmailRecruteur);
}