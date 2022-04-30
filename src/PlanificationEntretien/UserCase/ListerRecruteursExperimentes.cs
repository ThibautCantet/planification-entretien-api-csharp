using System.Collections.Generic;
using System.Linq;
using PlanificationEntretien.Domain.Entities;
using PlanificationEntretien.Domain.Ports;

namespace PlanificationEntretien.UserCase;

public class ListerRecruteursExperimentes : IListerRecruteursExperimentes
{
    private readonly IRecruteurRepository _recruteurRepository;

    public ListerRecruteursExperimentes(IRecruteurRepository recruteurRepository) => _recruteurRepository = recruteurRepository;

    public IEnumerable<IRecruteurExperimente> Execute()
    {
        return _recruteurRepository.FindAll().Where(recruteur => recruteur.Xp >= 10)
            .Select(recruteur => new RecruteurExperimente(recruteur.Id, recruteur.Language, recruteur.Xp.Value, recruteur.Email));
    }
}