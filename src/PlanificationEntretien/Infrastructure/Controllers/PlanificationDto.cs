using System;

namespace PlanificationEntretien.Infrastructure.Controllers
{
    public record PlanificationDto(Guid CandidatId, Guid RecruteurId, DateTime DisponibiliteCandidat, DateTime DisponibiliteRecruteur);
}