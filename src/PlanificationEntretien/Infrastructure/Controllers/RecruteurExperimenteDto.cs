using System;
using PlanificationEntretien.Domain.Entities;

namespace PlanificationEntretien.Infrastructure.Controllers;

public record RecruteurExperimenteDto(Guid Id, String Competence, string Email) : IRecruteurExperimente;