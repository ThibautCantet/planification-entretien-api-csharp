using System;
using System.Collections.Generic;
using PlanificationEntretien.Models;

namespace PlanificationEntretien.Services
{
    public interface IEntretienService
    {
        Entretien Planifier(Guid candidat, Guid recruteur, DateTime disponibiliteCandidat,
            DateTime disponibiliteRecruteur);

        IEnumerable<Entretien> Lister();
    }
}