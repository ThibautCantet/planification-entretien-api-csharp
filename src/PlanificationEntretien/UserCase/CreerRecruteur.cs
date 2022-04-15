using System;
using System.Text.RegularExpressions;
using PlanificationEntretien.Domain;

namespace PlanificationEntretien.UserCase;

public class CreerRecruteur : ICreerRecruteur
{
    private readonly IRecruteurRepository _recruteurRepository;

    public CreerRecruteur(IRecruteurRepository recruteurRepository)
    {
        _recruteurRepository = recruteurRepository;
    }

    public Recruteur Execute(string language, string email, int? xp)
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

        return _recruteurRepository.Save(new Recruteur(Guid.NewGuid(), language, email, xp));
    }
}