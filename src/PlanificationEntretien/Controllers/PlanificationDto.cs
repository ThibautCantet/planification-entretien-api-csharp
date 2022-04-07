using System;
using PlanificationEntretien;
using PlanificationEntretien.Models;

namespace PlanificationEntretien.Controllers
{
    public record PlanificationDto(Candidat Candidat, Recruteur Recruteur, DateTime DisponibiliteCandidat, DateTime DisponibiliteRecruteur)
    {
    }
}