using System;
using PlanificationEntretien;

namespace MasterClass.WebApi.Controllers
{
    public record PlanificationDto(Candidat Candidat, Recruteur Recruteur, DateTime DisponibiliteCandidat, DateTime DisponibiliteRecruteur)
    {
        public Candidat Candidat { get; } = Candidat;
        public Recruteur Recruteur { get; } = Recruteur;
        public DateTime DisponibiliteCandidat { get; } = DisponibiliteCandidat;
        public DateTime DisponibiliteRecruteur { get; } = DisponibiliteRecruteur;
    }
}