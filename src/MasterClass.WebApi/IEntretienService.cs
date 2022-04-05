using System;
using PlanificationEntretien;

namespace MasterClass.WebApi
{
    public interface IEntretienService
    {
        Entretien planifier(Candidat candidat, Recruteur recruteur, DateTime disponibiliteCandidat,
            DateTime disponibiliteRecruteur);
    }
}