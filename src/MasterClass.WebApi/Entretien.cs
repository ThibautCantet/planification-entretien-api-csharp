using System;

namespace PlanificationEntretien
{
    public record Entretien(DateTime DateEtHeure, string EmailCandidat, string EmailRecruteur);
}