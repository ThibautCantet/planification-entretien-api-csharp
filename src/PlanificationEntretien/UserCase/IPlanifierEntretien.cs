using System;
using PlanificationEntretien.Domain;

namespace PlanificationEntretien.UserCase
{
    public interface IPlanifierEntretien
    {
        Entretien Execute(Guid candidat, Guid recruteur, DateTime disponibiliteCandidat,
            DateTime disponibiliteRecruteur);
    }
}