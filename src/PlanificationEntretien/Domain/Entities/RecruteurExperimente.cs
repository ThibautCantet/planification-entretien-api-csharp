using System;

namespace PlanificationEntretien.Domain.Entities;

public class RecruteurExperimente : IRecruteurExperimente
{
    public Guid Id { get; }
    public string Competence { get; }
    public string Email { get; }

    public RecruteurExperimente(Guid id, String language, int xp, String email)
    {
        Id = id;
        Competence = String.Format("{0} {1} ans XP", language, xp);
        Email = email;
    }

}