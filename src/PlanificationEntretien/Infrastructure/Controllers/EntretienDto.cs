using System;

namespace PlanificationEntretien.Infrastructure.Controllers;

public record EntretienDto(Guid Id, DateTime DateEtHeure, string EmailCandidat, string EmailRecruteur);