using System.Collections.Generic;
using PlanificationEntretien.Domain.Entities;

namespace PlanificationEntretien.UserCase;

public interface IListerRecruteursExperimentes
{
    IEnumerable<IRecruteurExperimente> Execute();
}