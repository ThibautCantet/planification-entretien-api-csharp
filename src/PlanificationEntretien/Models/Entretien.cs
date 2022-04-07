using System;

namespace PlanificationEntretien.Models
{
    public record Entretien(Guid Id, DateTime DateEtHeure, string EmailCandidat, string EmailRecruteur);
}