using System;
using PlanificationEntretien;

namespace MasterClass.WebApi.Controllers
{
    public record PlanificationDto(Candidat Candidat, Recruteur Recruteur, DateTime DisponibiliteCandidat, DateTime DisponibiliteRecruteur)
    {
    }
}