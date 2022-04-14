using System;

namespace PlanificationEntretien.Domain
{
    public interface IPlanifierEntretien
    {
        Entretien Execute(Guid candidat, Guid recruteur, DateTime disponibiliteCandidat,
            DateTime disponibiliteRecruteur);
    }
}