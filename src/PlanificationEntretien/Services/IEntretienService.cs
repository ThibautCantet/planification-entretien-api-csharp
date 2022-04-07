using System;
using PlanificationEntretien;
using PlanificationEntretien.Models;

namespace PlanificationEntretien.Services
{
    public interface IEntretienService
    {
        Entretien Planifier(Candidat candidat, Recruteur recruteur, DateTime disponibiliteCandidat,
            DateTime disponibiliteRecruteur);
    }
}