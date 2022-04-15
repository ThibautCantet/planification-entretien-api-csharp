using System;
using System.Text.RegularExpressions;
using PlanificationEntretien.Domain;
using PlanificationEntretien.Domain.Entities;
using PlanificationEntretien.Domain.Ports;

namespace PlanificationEntretien.UserCase;

public class CreerCandidat : ICreerCandidat
{
    private readonly ICandidatRepository _candidatRepository;

    public CreerCandidat(ICandidatRepository candidatRepository)
    {
        _candidatRepository = candidatRepository;
    }

    public Candidat Execute(string language, string email, int? xp)
    {
        String EMAIL_REGEX = "^[\\w-_.+]*[\\w-_.]@([\\w]+\\.)+[\\w]+[\\w]$";
        MatchCollection mc = Regex.Matches(email, EMAIL_REGEX);
        if (mc.Count == 0 
            || String.IsNullOrEmpty(language)
            || String.IsNullOrEmpty(email)
            || !xp.HasValue
            || xp < 0)
        {
            return null;
        }

        return _candidatRepository.Save(new Candidat(Guid.NewGuid(), language, email, xp));
    }
}