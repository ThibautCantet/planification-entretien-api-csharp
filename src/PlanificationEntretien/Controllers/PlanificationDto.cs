using System;
using PlanificationEntretien;
using PlanificationEntretien.Models;

namespace PlanificationEntretien.Controllers
{
    public record PlanificationDto(Guid CandidatId, Guid RecruteurId, DateTime DisponibiliteCandidat, DateTime DisponibiliteRecruteur)
    {
    }
}