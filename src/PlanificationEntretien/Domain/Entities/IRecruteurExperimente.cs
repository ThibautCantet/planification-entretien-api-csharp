using System;

namespace PlanificationEntretien.Domain.Entities;

public interface IRecruteurExperimente
{
    Guid Id { get; }
    string Competence { get; }
    string Email { get; }
}